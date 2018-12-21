using NewCarRental.Models.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NewCarRental.Helpers
{
    public class AdultCheckerAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var customer = (Customers)validationContext.ObjectInstance;

            if (customer.BirthDate == null || customer.BirthDate == new DateTime(1, 1, 1))
            {
                return new ValidationResult("Birthdate is required!");
            }

            var age = DateTime.Today.Year - customer.BirthDate.Year;

            return (age >= 18)
                ? ValidationResult.Success
                : new ValidationResult("Customer should be at least 18 years old to go on membership type and to rent a car.");
        }
    }
}