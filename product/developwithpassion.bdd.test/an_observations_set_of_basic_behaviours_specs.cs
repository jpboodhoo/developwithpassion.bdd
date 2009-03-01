using System;
using System.Collections.Generic;
using System.Data;
using bdddoc.core;
using developwithpassion.bdd.concerns;
using developwithpassion.bdd.contexts;
using developwithpassion.bdd.mbunit.standard;
using developwithpassion.bdd.mbunit.standard.observations;
using developwithpassion.bdd.mbunit;
using developwithpassion.commons.core.infrastructure.containers;
using MbUnit.Framework;
using Rhino.Mocks;

namespace developwithpassion.bdd.test
{
    public class an_observations_set_of_basic_behaviours_specs
    {
        [Observations]
        public abstract class concern_for_an_observations_set_of_basic_behaviours
        {
            protected SampleSetOfObservations sut;

            [SetUp]
            public void setup()
            {
                SampleSetOfObservations.reset();
                sut = new SampleSetOfObservations();
                an_observations_set_of_basic_behaviours<IDbConnection>.sut = MockRepository.GenerateMock<IDbConnection>();
                establish_context();
                because();
            }

            protected virtual void establish_context() {}
            protected abstract void because();
        }

        [Concern(typeof (an_observations_set_of_basic_behaviours<>))]
        public class when_told_to_setup : concern_for_an_observations_set_of_basic_behaviours
        {
            protected override void because()
            {
                sut.setup();
            }


            [Observation]
            public void should_clear_the_dependencies_dictionary()
            {
                an_observations_set_of_basic_behaviours<IDbConnection>.dependencies.should_not_be_null();
                an_observations_set_of_basic_behaviours<IDbConnection>.dependencies.Count.should_be_equal_to(0);
            }

            [Observation]
            public void should_run_the_context_block()
            {
                SampleSetOfObservations.context_block_ran.should_be_true();
            }

            [Observation]
            public void should_run_the_because_blocks()
            {
                an_observations_set_of_basic_behaviours<IDbConnection>.sut.was_told_to(x => x.Open());
            }

            [Observation]
            public void should_not_run_the_after_each_observation_block()
            {
                SampleSetOfObservations.after_each_observation_block_ran.should_be_false();
            }
        }

        public abstract class concern_for_an_observations_set_of_basic_behaviours_that_has_run_its_setup : concern_for_an_observations_set_of_basic_behaviours
        {
            protected override void establish_context()
            {
                sut.setup();
            }
        }

        [Concern(typeof (an_observations_set_of_basic_behaviours<>))]
        public class when_told_to_teardown : concern_for_an_observations_set_of_basic_behaviours
        {
            protected override void establish_context()
            {
                an_observations_set_of_basic_behaviours<IDbConnection>.dependencies = new Dictionary<Type, object>();
            }

            protected override void because()
            {
                sut.tear_down();
            }

            [Observation]
            public void should_run_the_after_each_observation_block()
            {
                SampleSetOfObservations.after_each_observation_block_ran.should_be_true();
            }

            [Observation]
            public void should_not_run_the_context_and_because_and_after_sut_has_been_initialized_blocks()
            {
                SampleSetOfObservations.context_block_ran.should_be_false();
                SampleSetOfObservations.after_the_sut_has_been_created_block_ran.should_be_false();
                an_observations_set_of_basic_behaviours<IDbConnection>.sut.was_never_told_to(x => x.Open());
            }
        }

        [Concern(typeof (an_observations_set_of_basic_behaviours<>))]
        public class when_it_is_asked_for_the_exception_that_was_thrown : concern_for_an_observations_set_of_basic_behaviours_that_has_run_its_setup
        {
            static Exception exception = new Exception();
            static bool alternate_because_block_ran;
            Exception result;

            static Action action = () =>
            {
                alternate_because_block_ran = true;
                throw exception;
            };

            protected override void establish_context()
            {
                an_observations_set_of_basic_behaviours<IDbConnection>.doing(action);
            }

            protected override void because()
            {
                result = an_observations_set_of_basic_behaviours<IDbConnection>.exception_thrown_by_the_sut;
            }

            [Observation]
            public void should_run_the_code_in_the_because_action()
            {
                alternate_because_block_ran.should_be_true();
            }

            [Observation]
            public void should_return_the_exception_throw_in_the_alternate_because_block()
            {
                result.should_be_equal_to(exception);
            }
        }

        public class SampleSetOfObservations : an_observations_set_of_basic_behaviours<IDbConnection>
        {
            static public bool context_block_ran;
            static public bool after_each_observation_block_ran;
            static public bool after_the_sut_has_been_created_block_ran;

            static public void reset()
            {
                context_block_ran = false;
                after_each_observation_block_ran = false;
                after_the_sut_has_been_created_block_ran = false;
                sut = null;
            }

            static context c = () =>
            {
                context_block_ran = true;
            };

            public override IDbConnection create_sut()
            {
                return an<IDbConnection>();
            }

            after_the_sut_has_been_created i = () =>
            {
                after_the_sut_has_been_created_block_ran = true;
            };

            public after_each_observation a = () =>
            {
                after_each_observation_block_ran = true;
            };

            because b = () =>
            {
                sut.Open();
            };
        }

        [Concern(typeof (an_observations_set_of_basic_behaviours<>))]
        public class when_its_doing_method_is_leveraged : concern_for_an_observations_set_of_basic_behaviours
        {
            static Action action = () => {};

            protected override void because()
            {
                an_observations_set_of_basic_behaviours<IDbConnection>.doing(action);
            }

            [Observation]
            public void should_store_the_action_as_the_because_action()
            {
                an_observations_set_of_basic_behaviours<IDbConnection>.behaviour_performed_in_because.should_be_equal_to(action);
            }
        }

        [Concern(typeof (an_observations_set_of_basic_behaviours<>))]
        public class when_its_an_method_is_used_to_create_a_mock : concern_for_an_observations_set_of_basic_behaviours
        {
            IDbConnection result;
            object result2;

            protected override void because()
            {
                result = an_observations_set_of_basic_behaviours<IDbConnection>.an<IDbConnection>();
                result2 = an_observations_set_of_basic_behaviours<IDbConnection>.an(typeof (IDbConnection));
            }

            [Observation]
            public void should_create_a_mock_of_the_specific_type()
            {
                result.should_not_be_null();
                result2.should_be_an_instance_of<IDbConnection>();
            }
        }
    }

    [Concern(typeof (an_observations_set_of_basic_behaviours<>))]
    public class when_it_makes_use_of_a_container_dependency :
        observations_for_a_sut_without_a_contract<when_it_makes_use_of_a_container_dependency.SomeObjectWithContainerDependencies>
    {
        static IDbConnection connection;

        context c = () =>
        {
            connection = container_dependency<IDbConnection>();
        };

        because b = () =>
        {
            sut.do_something();
        };

        [Observation]
        public void the_sut_should_be_able_to_access_the_item_from_the_container()
        {
            connection.was_told_to(x => x.Open());
        }

        public class SomeObjectWithContainerDependencies
        {
            public void do_something()
            {
                Container.current.get_an<IDbConnection>().Open();
            }
        }
    }
}