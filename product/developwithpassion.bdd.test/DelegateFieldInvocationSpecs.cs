using bdddoc.core;
using developwithpassion.bdd.contexts;
using developwithpassion.bdd.core.commands;
using developwithpassion.bdd.mbunit.standard.observations;
using developwithpassion.bdd.mbunit;

namespace developwithpassion.bdd.test
{
    public class DelegateFieldInvocationSpecs
    {
        public abstract class concern_for_delegate_field_invocation : observations_for_a_sut_with_a_contract<ICommand,
                                                                          DelegateFieldInvocation> {}

        [Concern(typeof (DelegateFieldInvocation))]
        public class when_a_delegate_field_invocation_is_run : concern_for_delegate_field_invocation
        {
            static SomeClassWithDelegateFields target;

            context c = () =>
            {
                target = new SomeClassWithDelegateFields();
                target.reset();
            };

            because b = () => sut.run();


            it should_invoke_all_of_the_delegates_in_the_taget_class_for_the_specific_delegate_type = () =>
            {
                SomeClassWithDelegateFields.because_block_invocation_count.should_be_equal_to(2);
                SomeClassWithDelegateFields.after_each_observation_block_invocation_count.should_be_equal_to(0);
                SomeClassWithDelegateFields.context_block_invocation_count.should_be_equal_to(0);
            };

            public override ICommand create_sut()
            {
                return new DelegateFieldInvocation(typeof (because), target, typeof (SomeClassWithDelegateFields));
            }
        }


        public class SomeClassWithDelegateFields
        {
            public void reset()
            {
                because_block_invocation_count = 0;
                after_each_observation_block_invocation_count = 0;
                context_block_invocation_count = 0;
            }

            because b = () =>
            {
                because_block_invocation_count++;
            };

            because b2 = () =>
            {
                because_block_invocation_count++;
            };

            context c = () =>
            {
                context_block_invocation_count ++;
            };

            context c2 = () =>
            {
                context_block_invocation_count ++;
            };

            after_each_observation a = () =>
            {
                after_each_observation_block_invocation_count++;
            };

            after_each_observation a2 = () =>
            {
                after_each_observation_block_invocation_count++;
            };

            static public int because_block_invocation_count;
            static public int after_each_observation_block_invocation_count;
            static public int context_block_invocation_count;
        }
    }
}