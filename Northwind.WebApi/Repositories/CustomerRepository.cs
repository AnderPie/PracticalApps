using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Caching.Memory;
using Northwind.EntityModels;

namespace Northwind.WebApi.Repositories
{
    /*
     * Types of caches
     * 
     * Singleton: Created the first time they are requested, then shared OR an instance is provided at time of registration.
     * Scoped: Once per client request, disposed when response returns to client. Use when data needs to persist through a request.
     * Transient: Service created each time its requested. Should be lightweight and stateless. Used when data is needed immediately then can be discarded.
     * 
     */

    public class CustomerRepository : ICustomerRepository
    {
        private readonly IMemoryCache _memoryCache;
        private readonly MemoryCacheEntryOptions _cacheEntryOptions = new()
        {
            SlidingExpiration = TimeSpan.FromMinutes(30) // Lives for 30 minutes from instantiation
        };
        // Use an instance data context field because it should not be cached due to the data context having internal caching
        private NorthwindContext _db;
        public CustomerRepository(NorthwindContext db, IMemoryCache memoryCache)
        {
            _db = db;
            _memoryCache = memoryCache;
        }

        public async Task<Customer?> CreateAsync(Customer c)
        {
            c.CustomerId = c.CustomerId.ToUpper(); // Normalize to uppercase
            // Add to database using EF Core.
            EntityEntry<Customer> added = await _db.Customers.AddAsync(c);
            int affected = await _db.SaveChangesAsync();
            if (affected == 1)
            {
                // If saved to database then store in cache
                _memoryCache.Set(c.CustomerId, c, _cacheEntryOptions);
                return c;
            }
            return null;
        }

        public async Task<Customer[]> RetrieveAllAsync()
        {
            return await _db.Customers.ToArrayAsync();
        }

        // Uses the cache if possible
        public Task<Customer?> RetrieveAsync(string id)
        {
            id = id.ToUpper(); // Normalize to uppercase

            // Try to get from cache
            if(_memoryCache.TryGetValue(id, out Customer? fromCache)) return Task.FromResult(fromCache);
            // If not in cache, try to get from the database
            Customer? fromDb = _db.Customers.FirstOrDefault(c => c.CustomerId == id);
            // If not in database, then return null result
            if (fromDb is null) return Task.FromResult(fromDb);
            // If in the database, store in the cache and return customer
            _memoryCache.Set(fromDb.CustomerId, fromDb, _cacheEntryOptions);
            return Task.FromResult(fromDb);
        }

        public async Task<Customer?> UpdateAsync(Customer c)
        {
            c.CustomerId = c.CustomerId.ToUpper(); // Normalization

            _db.Customers.Update(c);

            int affected = await _db.SaveChangesAsync();
            if (affected == 1)
            {
                _memoryCache.Set(c.CustomerId, c, _cacheEntryOptions);
                return c;
            }
            return null;
        }

        public async Task<bool?> DeleteAsync(string id)
        {
            id = id.ToUpper();
            Customer? c = await _db.Customers.FindAsync(id);
            if (c is null) return null;
            _db.Customers.Remove(c);
            int affected = await _db.SaveChangesAsync();
            if (affected == 1)
            {
                _memoryCache.Remove(c.CustomerId);
                return true;
            }
            return null;
        }
    }
}
