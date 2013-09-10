using System.Configuration;
using System.Net;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

namespace EventServer 
{
    using NServiceBus;

	/*
		This class configures this endpoint as a Server. More information about how to configure the NServiceBus host
		can be found here: http://particular.net/articles/profiles-for-nservicebus-host
	*/
	public class EndpointConfig : IConfigureThisEndpoint, AsA_Server, IWantCustomInitialization
    {
        public void EndPointConfig()
        {
            
        }

	    public void Init()
	    {
            IUnityContainer container = new UnityContainer();
            var section = (UnityConfigurationSection)ConfigurationManager.GetSection("Unity");
            section.Configure(container);

            Configure.With().UnityBuilder(container);
	    }
    }
}