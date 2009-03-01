using bdddoc.core;
using developwithpassion.bdd.contexts;
using developwithpassion.bdd.mbunit.standard.observations;
using developwithpassion.bdd.mbunit;

namespace developwithpassion.bdd.test
{
    public class WaitSpecs
    {
        public abstract class concern_for_wait : observations_for_a_static_sut {}

        [Concern(typeof (Wait))]
        public class when_it_waits_past_the_allowed_timeout : concern_for_wait
        {
            because b = () =>
                        doing(() => Wait.until(() => false, 20));

            it should_throw_an_exception = () =>
                                           exception_thrown_by_the_sut.should_not_be_null();
        }

        [Concern(typeof (Wait))]
        public class when_the_condition_it_is_waiting_on_has_been_satisfied : concern_for_wait
        {
            static bool finished;

            because b = () =>
            {
                Wait.until(() => true, 20);
                finished = true;
            };

            it should_carry_on_processing_code_after_the_wait = () =>
                                                                finished.should_be_true();
        }
    }
}