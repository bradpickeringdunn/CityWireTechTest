using App.DataAccess;
using App.Models;
using App.Rules;
using App.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App
{
    public class CustomerService
    {
        private ICustomerCreditServiceChannel customerCreditService;
        private ICompanyRepository companyRepository;
        private ICustomerDataAccess customerDataAccess;

        public CustomerService(ICustomerCreditServiceChannel customerCreditService, ICompanyRepository companyRepository, ICustomerDataAccess customerDataAccess)
        {
            this.customerCreditService = customerCreditService;
            this.companyRepository = companyRepository;
            this.customerDataAccess = customerDataAccess;
        }

        public bool AddCustomer(Customer customer, int companyId)
        {
            var addedCustomerResult = false;

            if (ValidateCustomer.CustomerValid(customer))
            {
                var company = companyRepository.GetById(companyId);

                customer.Company = company;

                if (company.Name == "VeryImportantClient")
                {
                    // Skip credit check
                    customer.HasCreditLimit = false;
                }
                else if (company.Name == "ImportantClient")
                {
                    // Do credit check and double credit limit
                    customer.HasCreditLimit = true;
                    using (customerCreditService)
                    {
                        var creditLimit = customerCreditService.GetCreditLimit(customer.Firstname, customer.Surname, customer.DateOfBirth);
                        creditLimit = creditLimit * 2;
                        customer.CreditLimit = creditLimit;
                    }
                }
                else
                {
                    // Do credit check
                    customer.HasCreditLimit = true;
                    using ((IDisposable)customerCreditService)
                    {
                        var creditLimit = customerCreditService.GetCreditLimit(customer.Firstname, customer.Surname, customer.DateOfBirth);
                        customer.CreditLimit = creditLimit;
                    }
                }

                if (customer.HasCreditLimit && customer.CreditLimit < 500)
                {
                    return false;
                }

                customerDataAccess.AddCustomer(customer);

                return true;

            }

            return addedCustomerResult;
        }
    }
}
