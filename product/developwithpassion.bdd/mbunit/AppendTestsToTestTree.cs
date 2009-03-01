using System.Linq;
using System.Reflection;
using developwithpassion.bdd.contexts;
using developwithpassion.bdd.core.commands;
using developwithpassion.bdd.core.extensions;

namespace developwithpassion.bdd.mbunit
{
    public class AppendTestsToTestTree : IParameterizedCommand<TestTreeArgs<it>>
    {
        const BindingFlags binding_flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy | BindingFlags.Instance;

        public void run_against(TestTreeArgs<it> item)
        {
            var delegate_fields = item.type_that_contains_tests.GetFields(binding_flags).Where(field => field.FieldType == typeof(it));
            delegate_fields.each(field => item.tree.AddChild(item.parent, new DelegateRunInvoker(item.run, field)));
        }
    }
}