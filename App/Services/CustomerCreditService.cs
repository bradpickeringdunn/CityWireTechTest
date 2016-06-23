using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace App.Services
{
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class CustomerCreditServiceClient : ClientBase<ICustomerCreditService>, ICustomerCreditService
    {

        public CustomerCreditServiceClient()
        {
        }

        public CustomerCreditServiceClient(string endpointConfigurationName) :
            base(endpointConfigurationName)
        {
        }

        public CustomerCreditServiceClient(string endpointConfigurationName, string remoteAddress) :
            base(endpointConfigurationName, remoteAddress)
        {
        }

        public CustomerCreditServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) :
            base(endpointConfigurationName, remoteAddress)
        {
        }

        public CustomerCreditServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) :
            base(binding, remoteAddress)
        {
        }

        public int GetCreditLimit(string firstname, string surname, System.DateTime dateOfBirth)
        {
            return base.Channel.GetCreditLimit(firstname, surname, dateOfBirth);
        }
    }
}
