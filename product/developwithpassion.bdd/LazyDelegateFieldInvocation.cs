using System;
using developwithpassion.bdd.core.commands;

namespace developwithpassion.bdd
{
    public class LazyDelegateFieldInvocation<DelegateType> : IParameterizedCommand<object>
    {
        Type target_type;

        public LazyDelegateFieldInvocation(Type target_type)
        {
            this.target_type = target_type;
        }

        public void run_against(object item)
        {
            new DelegateFieldInvocation(typeof (DelegateType), item, target_type).run();
        }
    }
}