using System;
using System.Collections.Generic;

namespace MicroCqrs.Classes.Internal
{
    internal class CqrsPipeline
    {
        internal CqrsPipeline(Type serviceType, List<Type> pipeline)
        {
            ServiceType = serviceType;
            Pipeline = pipeline;
        }
        
        internal Type ServiceType { get; }
        internal List<Type> Pipeline { get; }
    }
}