using System;
using System.Collections.Generic;
using developwithpassion.bdd.core.commands;

namespace developwithpassion.bdd.mbunit
{
    public class RunAllDelegateBlocksInClassHierarchyTopDown<DelegateType> : IParameterizedCommand<TestTreeArgs<DelegateType>> {
        public void run_against(TestTreeArgs<DelegateType> item)
        {
            item.tree.AddChild(item.parent, new TestDelegateInvoker<DelegateType>(
                                                item.run, build_command_chain_to_target(item.type_that_contains_tests))); 
        }

        IParameterizedCommand<object> build_command_chain_to_target(Type type_that_contains_test)
        {
            var actions = new Stack<IParameterizedCommand<object>>();
            var current_class = type_that_contains_test;

            while (current_class != typeof(Object))
            {
                actions.Push(new LazyDelegateFieldInvocation<DelegateType>(current_class));
                current_class = current_class.BaseType;
            }

            return actions.as_command_chain();
        }
    }
}