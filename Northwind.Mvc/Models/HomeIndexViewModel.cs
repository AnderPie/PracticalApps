using Northwind.EntityModels; // To use Category, Product
namespace Northwind.Mvc.Models
{

    /*
     * 
     * Two types of models, entity models and view models. Entity models are models of data entities (think Entity Framework Core) that should be taken from databases 
     * and converted to .NET objects.
     * 
     * View models serve HTML or JSON responses to users. 
     * 
     * Oftentimes in models, a record is used for the view model because the view model should be immutable
     * 
     */
    public record HomeIndexViewModel(int VisitorCount, IList<Category> Categories, IList<Product> Products);
}
