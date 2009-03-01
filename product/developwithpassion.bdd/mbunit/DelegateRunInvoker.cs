using System;
using System.Collections;
using System.Reflection;
using developwithpassion.bdd.core.extensions;
using MbUnit.Core.Invokers;
using MbUnit.Core.Runs;

namespace developwithpassion.bdd.mbunit
{
    public class DelegateRunInvoker : RunInvoker
    {
        FieldInfo delegate_field;

        public DelegateRunInvoker(IRun generator, FieldInfo delegate_field) : base(generator)
        {
            this.delegate_field = delegate_field;
        }

        public override object Execute(object o, IList args)
        {
            var actual_delegate = delegate_field.GetValue(o).downcast_to<Delegate>();
            return actual_delegate.DynamicInvoke();
        }

        public override string Name
        {
            get { return delegate_field.Name; }
        }
    }
}