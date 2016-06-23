using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace App.Tests
{
    [TestClass]
    public class CustomerServiceTestFixture
    {
        [TestMethod]
        public void Ensure_A_Customer_Can_Be_Added()
        {

            var result = new CustomerService().AddCustomer("a", "b", "@.v", DateTime.Now.AddYears(-22), 1);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Ensure_A_Customer_Cant_Be_Added_If_Firstname_Invalid()
        {

            var result = new CustomerService().AddCustomer("", "b", "@.v", DateTime.Now.AddYears(-22), 1);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Ensure_A_Customer_Cant_Be_Added_If_Email_Invalid()
        {

            var result = new CustomerService().AddCustomer("a", "", "@.v", DateTime.Now.AddYears(-22), 1);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Ensure_A_Customer_Cant_Be_Added_If_Lastname_Invalid()
        {

            var result = new CustomerService().AddCustomer("a", "", ".v", DateTime.Now.AddYears(-22), 1);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Ensure_A_Customer_Cant_Be_Added_If_Age_Invalid()
        {

            var result = new CustomerService().AddCustomer("a", "a", ".v", DateTime.Now, 1);

            Assert.IsFalse(result);
        }
    }
}
