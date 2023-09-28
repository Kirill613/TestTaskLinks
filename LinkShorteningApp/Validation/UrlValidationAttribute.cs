using System.ComponentModel.DataAnnotations;

namespace LinkShorteningApp.Validation;

public class UrlValidationAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        bool isUrlValid = Uri.TryCreate(value?.ToString(), UriKind.Absolute, out var uriResult)
           && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

        if (!isUrlValid)
        {
            return new ValidationResult(ErrorMessage);
        }

        return ValidationResult.Success;
    }
}
