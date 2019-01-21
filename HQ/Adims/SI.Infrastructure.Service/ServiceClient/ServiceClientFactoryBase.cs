using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace SI.Infrastructure.Service
{
    public abstract class ServiceClientFactoryBase<T> where T : ICommunicationObject, new()
    {
        private T CreateServiceClientInternal(string appServer)
        {
            T serviceClient = default(T);

            string appServerUrl = ConfigurationManager.AppSettings[appServer];

            if (appServerUrl == null)
            {
                serviceClient = new T();
            }
            else
            {
                EndpointAddress endpointAddress = new EndpointAddress(string.Format("{0}{1}", appServerUrl, EndpointRelativeAddress));
                serviceClient = NewServiceClient(endpointAddress);
            }

            return serviceClient;
        }

        protected abstract string EndpointRelativeAddress { get; }

        protected abstract T NewServiceClient(EndpointAddress endpointAddress);

        public T CreateServiceClient()
        {
            T serviceClient = CreateServiceClientInternal("ApServer1");

            if (serviceClient.State == CommunicationState.Faulted)
            {
                serviceClient = CreateServiceClientInternal("ApServer2");
            }

            return serviceClient;
        }
    }
}
