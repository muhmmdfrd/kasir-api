using FluentValidation.Results;

namespace KasirApi.Core.Validators.Extensions
{
    public static class ValidationExtension
    {
        public static void ValidOrThrow(this ValidationResult result)
        {
            if (!result.IsValid)
            {
                var error = result.Errors.First();
                throw new ArgumentException(error.ErrorMessage, error.PropertyName);
            }
        }

        public static void ValidOrThrow(this ValidationResult result, Exception ex)
        {
            if (!result.IsValid)
            {
                var error = result.Errors.First();
                throw ex;
            }
        }
    }
}
