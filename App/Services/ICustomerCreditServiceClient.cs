using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace App.Services
{
    [GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [ServiceContractAttribute(ConfigurationName = "App.ICustomerCreditService")]
    public interface ICustomerCreditService
    {

        [OperationContractAttribute(Action = "http://tempuri.org/ICustomerCreditService/GetCreditLimit", ReplyAction = "http://tempuri.org/ICustomerCreditService/GetCreditLimitResponse")]
        int GetCreditLimit(string firstname, string surname, System.DateTime dateOfBirth);
    }
}
