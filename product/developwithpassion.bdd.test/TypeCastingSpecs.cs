using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using bdddoc.core;
using developwithpassion.bdd.contexts;
using developwithpassion.bdd.core.extensions;
using developwithpassion.bdd.mbunit.standard.observations;
using developwithpassion.bdd.mbunit;

namespace developwithpassion.bdd.test
{
    public class TypeCastingSpecs
    {
        public abstract class concern_for_type_casting : observations_for_a_static_sut {}

        [Concern(typeof (TypeCasting))]
        public class when_a_legitimate_downcast_is_made : concern_for_type_casting
        {
            it should_retrieve_the_object_back_downcasted_to_the_target_type = () =>
            {
                IList<int> list = new List<int>();
                var to = list.downcast_to<List<int>>();
            };
        }

        [Concern(typeof (TypeCasting))]
        public class when_asking_if_an_object_is_not_an_instance_of_a_specific_type : concern_for_type_casting
        {
            it should_make_determination_based_on_whether_the_object_is_assignable_from_the_specific_type = () =>
            {
                new SqlConnection().is_not_a<IDbCommand>().should_be_true();
                new SqlConnection().is_not_a<IDbConnection>().should_be_false();
            };
        }
    }
}