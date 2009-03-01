using System.Collections.Generic;
using bdddoc.core;
using developwithpassion.bdd.contexts;
using developwithpassion.bdd.core.extensions;
using developwithpassion.bdd.mbunit.standard.observations;
using developwithpassion.bdd.mbunit;

namespace developwithpassion.bdd.test
{
    public class IterationSpecs
    {
        public abstract class concern_for_iteration_extensions : observations_for_a_static_sut {}

        [Concern(typeof (Iteration))]
        public class when_generating_a_range_of_numbers : concern_for_iteration_extensions
        {
            static IEnumerable<int> result;

            because b = () =>
            {
                result = 1.to(2);
            };

            it should_return_an_enumerable_of_numbers_for_each_number_in_the_range = () =>
            {
                var numbers_visited = 0;
                foreach (var number in result) numbers_visited++;
                numbers_visited.should_be_equal_to(2);
            };
        }
    }
}