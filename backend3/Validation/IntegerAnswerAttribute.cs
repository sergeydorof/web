using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Backend3.Validation
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
    public sealed class IntegerAnswerAttribute : ValidationAttribute
    {
        public int MinValue { get; set; } = int.MinValue;
        public int MaxValue { get; set; } = int.MaxValue;

        public IntegerAnswerAttribute() : base("The {0} must be a whole number, e.g. -2 or 14.")
        {
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var text = (value as string)?.Trim();
            if (string.IsNullOrEmpty(text))
            {
                return ValidationResult.Success;
            }

            var members = validationContext.MemberName != null
                ? new[] { validationContext.MemberName }
                : null;

            if (!int.TryParse(text, NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture, out var number))
            {
                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName), members);
            }

            if (number < MinValue || number > MaxValue)
            {
                return new ValidationResult(
                    $"The {validationContext.DisplayName} must be between {MinValue} and {MaxValue}.",
                    members);
            }

            return ValidationResult.Success;
        }
    }
}
