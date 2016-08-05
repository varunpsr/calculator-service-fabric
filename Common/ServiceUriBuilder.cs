﻿using System;
using System.Fabric;

namespace Common
{
    public class ServiceUriBuilder
    {
        public ServiceUriBuilder(string serviceInstance)
        {
            ServiceInstance = serviceInstance;
        }

        public ServiceUriBuilder(string applicationInstance, string serviceInstance)
        {
            ApplicationInstance = applicationInstance;
            ServiceInstance = serviceInstance;
        }

        /// <summary>
        /// The name of the application instance that contains he service.
        /// </summary>
        public string ApplicationInstance { get; set; }

        /// <summary>
        /// The name of the service instance.
        /// </summary>
        public string ServiceInstance { get; set; }

        public Uri ToUri()
        {
            string applicationInstance = ApplicationInstance;

            if (String.IsNullOrEmpty(applicationInstance))
            {
                try
                {
                    // the ApplicationName property here automatically prepends "fabric:/" for us
                    applicationInstance = FabricRuntime.GetActivationContext().ApplicationName.Replace("fabric:/", String.Empty);
                }
                catch (InvalidOperationException)
                {
                    // FabricRuntime is not available. 
                    // This indicates that this is being called from somewhere outside the Service Fabric cluster.
                }
            }

            return new Uri("fabric:/" + applicationInstance + "/" + ServiceInstance);
        }
    }
}
