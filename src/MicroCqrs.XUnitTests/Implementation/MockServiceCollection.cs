using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;

namespace MicroCqrs.XUnitTests.Implementation
{
    [ExcludeFromCodeCoverage]
    public class MockServiceCollection : IServiceCollection
    {
        public List<ServiceDescriptor> AddedItems { get; } = new();
        
        public void Add(ServiceDescriptor item)
            => AddedItems.Add(item);
        
        
        public IEnumerator<ServiceDescriptor> GetEnumerator()
        {
            throw new System.NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        
        public void Clear()
        {
            throw new System.NotImplementedException();
        }

        public bool Contains(ServiceDescriptor item)
        {
            throw new System.NotImplementedException();
        }

        public void CopyTo(ServiceDescriptor[] array, int arrayIndex)
        {
            throw new System.NotImplementedException();
        }

        public bool Remove(ServiceDescriptor item)
        {
            throw new System.NotImplementedException();
        }

        public int Count { get; }
        public bool IsReadOnly { get; }
        public int IndexOf(ServiceDescriptor item)
        {
            throw new System.NotImplementedException();
        }

        public void Insert(int index, ServiceDescriptor item)
        {
            throw new System.NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new System.NotImplementedException();
        }

        public ServiceDescriptor this[int index]
        {
            get => throw new System.NotImplementedException();
            set => throw new System.NotImplementedException();
        }
    }
}