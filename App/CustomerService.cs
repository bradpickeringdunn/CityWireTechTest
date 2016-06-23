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
        private ICustomerCreditService customerCreditService;
        private ICompanyRepository companyRepository;

        public CustomerService(ICustomerCreditService customerCreditService, ICompanyRepository companyRepository)
        {
            this.customerCreditService = customerCreditService;
            this.companyRepository = companyRepository;
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
                    using (var customerCreditService = new CustomerCreditServiceClient())
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
                    using (var customerCreditService = new CustomerCreditServiceClient())
                    {
                        var creditLimit = customerCreditService.GetCreditLimit(customer.Firstname, customer.Surname, customer.DateOfBirth);
                        customer.CreditLimit = creditLimit;
                    }
                }

                if (customer.HasCreditLimit && customer.CreditLimit < 500)
                {
                    return false;
                }

                CustomerDataAccess.AddCustomer(customer);

                return true;

            }

            return addedCustomerResult;
        }
    }
}
