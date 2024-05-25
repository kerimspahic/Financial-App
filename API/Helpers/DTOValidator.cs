using System.ComponentModel.DataAnnotations;

namespace API.Helpers
{
    public class DTOValidator
    {
        public static List<ValidationResult> ValidateDTO(object dto)
        {
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(dto, serviceProvider: null, items: null);
            Validator.TryValidateObject(dto, validationContext, validationResults, validateAllProperties: true);
            return validationResults;
        }
    }
}