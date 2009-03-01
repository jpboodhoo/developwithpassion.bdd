using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using developwithpassion.bdd.core.commands;
using developwithpassion.bdd.core.extensions;

namespace developwithpassion.bdd
{
    public class DelegateFieldInvocation : ICommand
    {
        const BindingFlags probing_flags = BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.DeclaredOnly;
        Type delegate_type;
        readonly object instance;
        IEnumerable<FieldInfo> fields;

        public DelegateFieldInvocation(Type delegate_type, object instance, Type target_type)
        {
            fields = target_type.GetFields(probing_flags);
            this.delegate_type = delegate_type;
            this.instance = instance;
        }

        public void run()
        {
            all_fields_of_the_target_delegate_type().each(x => x.GetValue(instance).downcast_to<Delegate>().DynamicInvoke());
        }

        IEnumerable<FieldInfo> all_fields_of_the_target_delegate_type()
        {
            return fields.Where(x => x.FieldType.Equals(delegate_type));
        }
    }
}