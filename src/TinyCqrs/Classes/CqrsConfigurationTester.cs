using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TinyCqrs.Classes.Internal;

namespace TinyCqrs.Classes
{
    public class CqrsConfigurationTester
    {
        private InternalServiceCollection ServiceCollection { get; } = new();
        private Assembly Assembly { get; }

        public CqrsConfigurationTester(Assembly assembly = null)
            => Assembly = assembly != null ? assembly : Assembly.GetExecutingAssembly();
        
        public CqrsConfigurationTester(Type typeInAssembly)
            => Assembly = typeInAssembly.Assembly;

        public bool IsAssemblyNull()
            => Assembly == null;
        
        private void RunConfigureIfNotInitialized()
        {
            if (ServiceCollection.AddedItems.Count == 0)
                ServiceCollection.ConfigureCqrsObjects(Assembly);
        }
        
        /// <summary>
        /// Returns true if a query or command handler contract has been added to the service collection more than once,
        /// without Cqrs attributes being applied to prevent addition to the service collection.
        /// </summary>
        public bool HasDuplicateConfigurations()
        {
            RunConfigureIfNotInitialized();
            
            return ServiceCollection.AddedItems
                .Select(item => ServiceCollection.AddedItems.Count(x => x.ServiceType == item.ServiceType))
                .Any(count => count > 1);
        }

        /// <summary>
        /// Returns an ordered list of configured decorator classes and the core handler, based on the service interface requested.
        /// </summary>
        public List<Type> GetCqrsPipeline(Type serviceType)
        {
            var registrar = new HandlerRegistrar();
            ServiceCollection.ConfigureCqrsObjects(Assembly, registrar);
            
            return registrar.PipelineRegistrations
                .Where(x => x.ServiceType == serviceType)
                .SelectMany(x => x.Pipeline)
                .ToList();
        }
        
        /// <summary>
        /// Returns an ordered list of configured decorator classes and the core handler, based on the service interface requested.
        /// </summary>
        public List<Type> GetCqrsPipeline<T>()
            => GetCqrsPipeline(typeof(T));
    }
}