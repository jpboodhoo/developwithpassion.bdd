using bdddoc.core;
using developwithpassion.bdd.contexts;
using developwithpassion.bdd.core.extensions;
using developwithpassion.bdd.mbunit.standard.observations;
using developwithpassion.bdd.mbunit;

namespace developwithpassion.bdd.test
{
    public class StringExtensionsSpecs
    {
        [Concern(typeof (StringExtensions))]
        public class when_a_string_is_formatted_with_arguments : observations_for_a_static_sut
        {
            static string result;

            because b = () =>
                        result = "this is the {0};".format_using(1);

            it should_return_the_string_formatted_with_the_arguments = () =>
                                                                       result.should_be_equal_to("this is the 1;");
        }
    }
}