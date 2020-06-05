using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TEST.Models
{
    public class Min18YearsIfAMember : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var customer = validationContext.ObjectInstance as Customer;

            Enum.TryParse(customer.MembershipTypeId.ToString(), out Customer.Membership membershipType);

            if (membershipType == Customer.Membership.Default || membershipType == Customer.Membership.PayAsYouGo)
            {
                return ValidationResult.Success;
            }

            if (customer.Birthday == null)
            {
                return new ValidationResult("Birthdate is required.");
            }

            var adult = customer.Birthday.Value.AddYears(18) <= DateTime.Now;

            return adult
                ? ValidationResult.Success
                : new ValidationResult("Customer should be at least 18 years old to go on a member.");
        }
    }
}