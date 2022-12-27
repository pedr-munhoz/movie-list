using System.ComponentModel.DataAnnotations;

namespace MovieListApi.Tests.Models.ViewModels;

public class TestModelHelper
{
    public static (bool isValid, IList<ValidationResult> errors) Validate(object model)
    {
        var errors = new List<ValidationResult>();

        var validationContext = new ValidationContext(model, null, null);
        var isValid = Validator.TryValidateObject(model, validationContext, errors, true);

        var validableModel = model as IValidatableObject;

        if (validableModel is not null)
            validableModel.Validate(validationContext);

        return (isValid, errors);
    }
}
