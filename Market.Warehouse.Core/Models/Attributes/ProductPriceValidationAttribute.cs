using System.ComponentModel.DataAnnotations;

namespace Market.Warehouse.Domain.Models.Attributes;

public class ProductPriceValidationAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var product = validationContext.ObjectInstance as Product;
        if(product == null || product.Price <= 0)
        {
            return new ValidationResult("Price cannot be greater than zero");
        }
        return ValidationResult.Success;
    }
}
