using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERPBO.CustomValidationAttribute.Production
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class RequisitionForInProductionRequisitionAttr: ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            long? asssembly = (long?)validationContext.ObjectType.GetProperty("AssemblyLineId").GetValue(validationContext.ObjectInstance, null);

            long? packaging = (long?)validationContext.ObjectType.GetProperty("PackagingLineId").GetValue(validationContext.ObjectInstance, null);

            if((asssembly == null || asssembly == 0) && (packaging == null || packaging == 0))
                return new ValidationResult("At least one is required!!");

            return ValidationResult.Success;
        }
    }
}