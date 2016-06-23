using App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.Rules
{
    public static class ValidateCustomer
    {
        public static bool CustomerValid(Customer customer)
        {
            var validCustomer = ValidFirstname(customer.Firstname) &&
                        ValidSurname(customer.Surname) &&
                        ValidEmail(customer.EmailAddress) &&
                        ValidAge(customer.DateOfBirth);

            return validCustomer;
        }

        private static bool ValidAge(DateTime dateOfBirth)
        {
            var now = DateTime.Now;
            int age = now.Year - dateOfBirth.Year;
            if (now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day)) age--;

            return age < 21;

        }

        private static bool ValidEmail(string emailAddress)
        {
            return !emailAddress.Contains("@") && !emailAddress.Contains(".");
        }

        private static bool ValidSurname(string surname)
        {
            return string.IsNullOrEmpty(surname);
        }

        private static bool ValidFirstname(string firstname)
        {
            return string.IsNullOrEmpty(firstname);
        }
    }
}
