using Microsoft.Extensions.Options;

using System.ComponentModel.DataAnnotations;

using ToDoApi.Domain.Configurations;

namespace ToDoApi.Host.Attributes
{
    public class MaxPageSizeAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var settings = validationContext.GetService<IOptionsMonitor<AppSettings>>();
            var maxValue = settings?.CurrentValue.MaxPageSize ?? int.MaxValue;

            if((int)value > maxValue)
            {
                return new ValidationResult($"Max page size is {maxValue}");
            }

            return ValidationResult.Success;
        }
    }
}
