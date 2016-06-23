using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FakeItEasy;
using App.Services;
using App.DataAccess;
using App.Models;

namespace App.Tests
{
    [TestClass]
    public class CustomerServiceTestFixture
    {
        [TestMethod]
        public void Ensure_A_Customer_Can_Be_Added()
        {
            var customerCreditServiceChannel = A.Fake<ICustomerCreditServiceChannel>();
            var customerCreditService = A.Fake<ICustomerCreditService>();
            var companyRepository = A.Fake<ICompanyRepository>(); ;
            var customerDataAccess = A.Fake<ICustomerDataAccess>();

            var customer = new Customer()
            {
                DateOfBirth = DateTime.Now.AddYears(-22),
                EmailAddress = "@.v",
                Firstname = "a",
                Surname = "b"
            };

            A.CallTo(() => customerCreditServiceChannel.GetCreditLimit(null, null, DateTime.Now)).WithAnyArguments().Returns(1000);
            A.CallTo(() => companyRepository.GetById(1)).WithAnyArguments().Returns(new Company()
            {
                Classification = Classification.Bronze,
                Id = 1,
                Name = "blah"
            });
            A.CallTo(() => customerDataAccess.AddCustomer(null)).WithAnyArguments().DoesNothing();

            var result = new CustomerService(customerCreditServiceChannel, companyRepository, customerDataAccess).AddCustomer(customer, 1);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Ensure_A_Customer_Cant_Be_Added_If_Firstname_Invalid()
        {
            var customerCreditService = A.Fake<ICustomerCreditServiceChannel>();
            var companyRepository = A.Fake<ICompanyRepository>(); ;
            var customerDataAccess = A.Fake<ICustomerDataAccess>();

            var customer = new Customer()
            {
                DateOfBirth = DateTime.Now.AddYears(-22),
                EmailAddress = "@.v",
                Firstname = "",
                Surname = "b"
            };

            var result = new CustomerService(customerCreditService, companyRepository, customerDataAccess).AddCustomer(customer, 1);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Ensure_A_Customer_Cant_Be_Added_If_Email_Invalid()
        {
            var customerCreditService = A.Fake<ICustomerCreditServiceChannel>();
            var companyRepository = A.Fake<ICompanyRepository>(); ;
            var customerDataAccess = A.Fake<ICustomerDataAccess>();

            var customer = new Customer()
            {
                DateOfBirth = DateTime.Now.AddYears(-22),
                EmailAddress = "",
                Firstname = "a",
                Surname = "b"
            };

            var result = new CustomerService(customerCreditService, companyRepository, customerDataAccess).AddCustomer(customer, 1);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Ensure_A_Customer_Cant_Be_Added_If_Lastname_Invalid()
        {
            var customerCreditService = A.Fake<ICustomerCreditServiceChannel>();
            var companyRepository = A.Fake<ICompanyRepository>(); ;
            var customerDataAccess = A.Fake<ICustomerDataAccess>();

            var customer = new Customer()
            {
                DateOfBirth = DateTime.Now.AddYears(-22),
                EmailAddress = "@.v",
                Firstname = "a",
                Surname = ""
            };

            var result = new CustomerService(customerCreditService, companyRepository, customerDataAccess).AddCustomer(customer, 1);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Ensure_A_Customer_Cant_Be_Added_If_Age_Invalid()
        {
            var customerCreditService = A.Fake<ICustomerCreditServiceChannel>();
            var companyRepository = A.Fake<ICompanyRepository>(); ;
            var customerDataAccess = A.Fake<ICustomerDataAccess>();

            var customer = new Customer()
            {
                DateOfBirth = DateTime.Now,
                EmailAddress = "@.v",
                Firstname = "a",
                Surname = "b"
            };

            var result = new CustomerService(customerCreditService, companyRepository, customerDataAccess).AddCustomer(customer, 1);

            Assert.IsFalse(result);
        }
    }
}
