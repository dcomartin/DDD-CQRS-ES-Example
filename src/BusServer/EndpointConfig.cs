using System;
using System.Configuration;
using System.Net;
using CommonDomain.Persistence;
using EventStore.ClientAPI;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

namespace BusServer 
{
    using NServiceBus;

	/*
		This class configures this endpoint as a Server. More information about how to configure the NServiceBus host
		can be found here: http://particular.net/articles/profiles-for-nservicebus-host
	*/
	public class EndpointConfig : IConfigureThisEndpoint, AsA_Publisher, IWantCustomInitialization
	{
	    public void Init()
	    {
            IUnityContainer container = new UnityContainer();
            var section = (UnityConfigurationSection)ConfigurationManager.GetSection("Unity");
            section.Configure(container);

            var connection = EventStoreConnection.Create();
            connection.Connect(new IPEndPoint(IPAddress.Loopback, 1113));          
            container.RegisterInstance(connection);

            Configure.With().UnityBuilder(container);
	    }
    }
}