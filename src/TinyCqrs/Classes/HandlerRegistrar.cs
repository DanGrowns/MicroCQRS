using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using TinyCqrs.Attributes;
using TinyCqrs.Classes.Internal;
using TinyCqrs.Interfaces;

namespace TinyCqrs.Classes
{
    internal class HandlerRegistrar
    {
        private List<ConstructorInfo> LastConstructorInfo { get; } = new();
        internal List<CqrsPipeline> PipelineRegistrations { get; } = new();
        
        private Func<IServiceProvider, object> BuildPipeline(IEnumerable<Type> pipeline, Type interfaceType)
        {
            // TODO: Generic type handling currently does not work when the constructed type has a generic parameter.
            var ctors = pipeline
                .Select(x =>
                {
                    var type = x.IsGenericType ? x.MakeGenericType(interfaceType.GenericTypeArguments) : x;
                    return type.GetConstructors().Single();
                })
                .ToList();

            LastConstructorInfo.Clear();
            LastConstructorInfo.AddRange(ctors);

            Func<IServiceProvider, object> func = provider =>
            {
                object current = null;

                foreach (var ctor in ctors)
                {
                    var parameterInfos = ctor.GetParameters().ToList();

                    var parameters = GetParameters(parameterInfos, current, provider);

                    current = ctor.Invoke(parameters);
                }

                return current;
            };

            return func;
        }

        private object[] GetParameters(IReadOnlyList<ParameterInfo> parameterInfos, object current, IServiceProvider provider)
        {
            var result = new object[parameterInfos.Count];

            for (var i = 0; i < parameterInfos.Count; i++)
            {
                result[i] = GetParameter(parameterInfos[i], current, provider);
            }

            return result;
        }

        private object GetParameter(ParameterInfo parameterInfo, object current, IServiceProvider provider)
        {
            var parameterType = parameterInfo.ParameterType;

            if (IsHandlerInterface(parameterType))
                return current;

            var service = provider.GetService(parameterType);
            if (service != null)
                return service;

            throw new ArgumentException($"Type {parameterType} not found");
        }

        private static Type ToDecorator(object attribute)
        {
            var type = attribute.GetType();

            if (type == typeof(CqrsDecoratedBy))
            {
                var decorator = (CqrsDecoratedBy) attribute;
                return decorator.ConcreteDecorator;
            }

            return null;
        }

        private static bool IsHandlerInterface(Type type)
        {
            if (!type.IsGenericType)
                return false;

            var typeDefinition = type.GetGenericTypeDefinition();

            return typeDefinition == typeof(ICmdHandler<>) 
                   || typeDefinition == typeof(ICmdHandler<,>) 
                   
                   || typeDefinition == typeof(ICmdHandlerAsync<>)
                   || typeDefinition == typeof(ICmdHandlerAsync<,>)
                   
                   || typeDefinition == typeof(IQueryHandler<>)
                   || typeDefinition == typeof(IQueryHandler<,>)
                   
                   || typeDefinition == typeof(IQueryHandlerAsync<>)
                   || typeDefinition == typeof(IQueryHandlerAsync<,>);
        }
        
        internal IEnumerable<Type> GetHandlerTypes(Assembly targetAssembly)
            => targetAssembly.GetTypes()
                .Where(x => x.GetInterfaces().Any(IsHandlerInterface))
                .Where(x => x.GetCustomAttribute<CqrsDecorator>() == null)
                .ToList();
        
        internal void AddHandler(IServiceCollection services, Type type)
        {
            var ignorable = type.GetCustomAttribute<CqrsIgnore>();
            if (ignorable != null)
                return;

            var attributes = type.GetCustomAttributes(false);

            var pipeline = attributes
                .Select(ToDecorator)
                .Where(x => x != null)
                .Concat(new[] { type })
                .Reverse()
                .ToList();

            foreach (var interfaceType in type.GetInterfaces())
            {
                var isHandler = IsHandlerInterface(interfaceType);
                if (isHandler == false)
                    continue;
                
                var factory = BuildPipeline(pipeline, interfaceType);

                services.AddTransient(interfaceType, factory);
                
                //
                // Used to expose the list of CqrsDecorators and the core handler for the sake of unit tests
                //
                var countEmptyConstructors = LastConstructorInfo.Count(x => x.GetParameters().Length == 0);
                if (countEmptyConstructors > 1)
                    throw new ArgumentException("More than one handler in the pipeline contains an empty constructor.", interfaceType.FullName);
            
                var registrationPipeline = pipeline.ToList();
                registrationPipeline.Reverse();
            
                PipelineRegistrations.Add(new CqrsPipeline(interfaceType, registrationPipeline));
                
            }
        }
    }
}