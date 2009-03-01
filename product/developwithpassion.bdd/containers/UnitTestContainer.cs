using System;
using System.Collections.Generic;
using developwithpassion.commons.core.infrastructure.containers;

namespace developwithpassion.bdd.containers
{
    public class UnitTestContainer
    {
        static IContainer container;
        static IDictionary<Type, IContainerItemResolver> items;
        static object mutex = new object();


        static public void add_implementation_of<Interface>(Interface implementation)
        {
            do_in_initialized_container(
                () => items.Add(typeof (Interface), new SimpleContainerItemResolver(() => implementation)));
        }

        static void do_in_initialized_container(Action action)
        {
            lock (mutex)
            {
                if (container == null)
                {
                    items = new Dictionary<Type, IContainerItemResolver>();
                    container = new SimpleContainer(items);
                    Container.initialize_with(container);
                }
                action();
            }
        }


        static public void tear_down()
        {
            lock (mutex)
            {
                items = null;
                container = null;
                Container.initialize_with(null);
            }
        }
    }
}