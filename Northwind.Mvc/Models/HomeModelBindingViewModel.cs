using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Mono.TextTemplating;

namespace Northwind.Mvc.Models
{
    public record HomeModelBindingViewModel(Thing Thing, bool HasErrors, IEnumerable<string> ValidationErrors);
}
