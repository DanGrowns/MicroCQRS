using System;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using TinyCqrs.Interfaces;

namespace TinyCqrs.Classes
{
    public static class ExtensionMethods
    {
        // Internal overload for obtaining pipeline details in unit tests.
        internal static void ConfigureCqrsObjects(this IServiceCollection services, Assembly targetAssembly, HandlerRegistrar registrar)
        {
            foreach (var type in registrar.GetHandlerTypes(targetAssembly))
                registrar.AddHandler(services, type);
        }
        
        /// <summary>
        /// Add CQRS handlers to the project via Dependency Injection, Decorators using the attribute style will automatically be added.
        /// </summary>
        /// <param name="services">Microsoft DI Service Container</param>
        /// <param name="targetAssembly">The assembly where all of your handlers exist.</param>
        public static void ConfigureCqrsObjects(this IServiceCollection services, Assembly targetAssembly)
        {
            var registrar = new HandlerRegistrar();

            foreach (var type in registrar.GetHandlerTypes(targetAssembly))
                registrar.AddHandler(services, type);
        }
        
        /// <summary>
        /// Add CQRS handlers to the project via Dependency Injection, Decorators using the attribute style will automatically be added.
        /// </summary>
        /// <param name="services">Microsoft DI Service Container</param>
        /// <param name="typeInTargetAssembly">Any type in the assembly you wish to target, to find where all of your handlers exist.</param>
        public static void ConfigureCqrsObjects(this IServiceCollection services, Type typeInTargetAssembly)
            => ConfigureCqrsObjects(services, typeInTargetAssembly.Assembly);
        
        /// <summary>
        /// Add CQRS handlers to the project via Dependency Injection, Decorators using the attribute style will automatically be added.
        /// </summary>
        /// <param name="services">Microsoft DI Service Container</param>
        public static void ConfigureCqrsObjects<T>(this IServiceCollection services)
            => ConfigureCqrsObjects(services, typeof(T));
        
        /// <summary>
        /// Add CQRS handlers to the project via Dependency Injection (from the executing assembly),
        /// Decorators using the attribute style will automatically be added.
        /// </summary>
        /// <param name="services">Microsoft DI Service Container</param>
        public static void ConfigureCqrsObjects(this IServiceCollection services)
            => ConfigureCqrsObjects(services, Assembly.GetExecutingAssembly());
    }
}