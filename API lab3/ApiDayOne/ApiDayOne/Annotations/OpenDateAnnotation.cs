using System.ComponentModel.DataAnnotations;

namespace ApiDayOne.Annotations
{
	class OpenDateAnnotation : ValidationAttribute
	{

		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			if (value != null)
			{
				DateOnly date = (DateOnly)value;
				DateOnly Current = DateOnly.FromDateTime(DateTime.Today);
				if (date >= Current)
				{
					return new ValidationResult("OpenDate must be in the past..");
				}
			}

			return ValidationResult.Success!;
		}

	}
}
