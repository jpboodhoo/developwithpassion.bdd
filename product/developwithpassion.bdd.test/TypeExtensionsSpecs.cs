using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using bdddoc.core;
using developwithpassion.bdd.contexts;
using developwithpassion.bdd.core.extensions;
using developwithpassion.bdd.mbunit.standard.observations;
using developwithpassion.bdd.mbunit;

namespace developwithpassion.bdd.test
{
    public class TypeExtensionSpecs
    {
        public abstract class concern_for_type_extensions : observations_for_a_static_sut {}

        [Concern(typeof (TypeExtensions))]
        public class when_a_type_is_told_to_find_its_greediest_constructor : observations_for_a_static_sut
        {
            static ConstructorInfo result;

            because b = () =>
                        result = typeof (SomethingWithConstructors).greediest_constructor();

            it should_return_the_constructor_that_takes_the_most_arguments = () =>
                                                                             result.GetParameters().Count().should_be_equal_to(2);
        }

        [Concern(typeof (TypeExtensions))]
        public class when_a_generic_type_is_told_to_return_its_proper_name : concern_for_type_extensions
        {
            static string result;

            because b = () =>
                        result = typeof (List<int>).proper_name();

            it should_return_a_name_that_has_its_generic_type_arguments_expanded = () =>
                                                                                   result.should_be_equal_to("List`1<System.Int32>");
        }

        public class SomethingWithConstructors
        {
            public IDbConnection connection { get; set; }

            public IDbCommand command { get; set; }

            public SomethingWithConstructors(IDbConnection connection) {}

            public SomethingWithConstructors(IDbConnection connection, IDbCommand command)
            {
                this.connection = connection;
                this.command = command;
            }
        }
    }
}