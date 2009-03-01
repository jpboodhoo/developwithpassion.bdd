using System;
using System.Collections.Generic;
using developwithpassion.commons.core.infrastructure.containers;

namespace developwithpassion.bdd.containers
{
    public class SimpleContainer : IContainer
    {
        readonly IDictionary<Type, IContainerItemResolver> resolvers;

        public SimpleContainer(IDictionary<Type, IContainerItemResolver> resolvers)
        {
            this.resolvers = resolvers;
        }

        public Interface get_an<Interface>()
        {
            return (Interface) get_an(typeof (Interface));
        }

        public object get_an(Type dependency_type)
        {
            return get_resolver_for(dependency_type).resolve();
        }

        public IEnumerable<Contract> get_all<Contract>()
        {
            throw new NotImplementedException();
        }

        public void add_resolver_for<Interface>(IContainerItemResolver resolver)
        {
            resolvers.Add(typeof (Interface), resolver);
        }

        IContainerItemResolver get_resolver_for(Type type)
        {
            return resolvers[type];
        }

        public void add_resolver<T>(DependencyResolver resolver)
        {
            add_resolver_for<T>(new SimpleContainerItemResolver(resolver));
        }
    }
}