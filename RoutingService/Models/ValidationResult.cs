namespace RoutingService.Models
{
    public class ValidationResult
    {
        public ValidationResult(ValidationErrorType errorType, string errorMessage = null)
        {
            IsValid = errorType == ValidationErrorType.None;
            ErrorType = errorType;
            ErrorMessage = errorMessage;
        }

        public bool IsValid { get; private set; }
        public ValidationErrorType ErrorType { get; private set; }
        public string ErrorMessage { get; private set; }

        public static ValidationResult Success => new ValidationResult(ValidationErrorType.None);
    }
}
