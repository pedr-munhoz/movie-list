using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieListApi.Tests.Models.ViewModels;

public class TestModelHelper
{
    public static IList<ValidationResult> Validate(object model)
    {
        var errors = new List<ValidationResult>();
        var validationContext = new ValidationContext(model, null, null);
        Validator.TryValidateObject(model, validationContext, errors, true);

        var validableModel = model as IValidatableObject;

        if (validableModel is not null)
            validableModel.Validate(validationContext);

        return errors;
    }
}
