using System.ComponentModel.DataAnnotations; // To use [Range], [Required], [Email Address]
namespace Northwind.Mvc.Models
{
    public record Thing(
        [Range(1,10)] int? Id,
        [Required] string? Color,
        [EmailAddress] string? Email
    );
}
