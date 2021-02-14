using IconicFund.Helpers.Enums;
using IconicFund.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace IconicFund.Helpers.CustomValidation
{
    public class MainDepartmentExportingDateValidationAttribute : ValidationAttribute
    {

        private readonly string _ExportingType;

        public MainDepartmentExportingDateValidationAttribute(string ExportingType)
        {
            _ExportingType = ExportingType;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
           
            var currentValue = (DateTime?)value;

            var property = validationContext.ObjectType.GetProperty(_ExportingType);

            if (property == null)
                throw new ArgumentException("Property with this name not found");

            var comparisonValue = (int)property.GetValue(validationContext.ObjectInstance);

            if (comparisonValue == (int)ExportingTypes.External && currentValue == null)
                return new ValidationResult(Messages.RequiredField);

            return ValidationResult.Success;
        }
    }
}
