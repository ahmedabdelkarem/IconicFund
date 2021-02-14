using IconicFund.Helpers.Enums;
using IconicFund.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace IconicFund.Helpers.CustomValidation
{
    public class MainDepartmentExportingValidationAttribute : ValidationAttribute
    {
        private readonly string _ExportingType;

        public MainDepartmentExportingValidationAttribute(string ExportingType)
        {
            _ExportingType = ExportingType;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            
            var currentValue = (string)value;

            var property = validationContext.ObjectType.GetProperty(_ExportingType);

            if (property == null)
                throw new ArgumentException("Property with this name not found");

            var comparisonValue = (int)property.GetValue(validationContext.ObjectInstance);

            if (comparisonValue == (int)ExportingTypes.External && string.IsNullOrEmpty(currentValue))
                return new ValidationResult(Messages.RequiredField);

            return ValidationResult.Success;
        }

    }
}
