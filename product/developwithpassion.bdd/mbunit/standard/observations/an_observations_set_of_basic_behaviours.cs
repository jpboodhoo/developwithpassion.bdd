using System;
using System.Collections.Generic;
using developwithpassion.bdd.containers;
using developwithpassion.bdd.contexts;
using developwithpassion.bdd.core.commands;
using developwithpassion.bdd.mbunit.standard;
using developwithpassion.bdd.concerns;
using MbUnit.Framework;
using Rhino.Mocks;

namespace developwithpassion.bdd.concerns
{
    public interface IObservations {}

    [Observations]
    public abstract class an_observations_set_of_basic_behaviours<SUT> : IObservations
    {
        static public IDictionary<Type, object> dependencies;
        static public Exception exception_thrown_while_the_sut_performed_its_work;
        static public Action behaviour_performed_in_because;
        static public SUT sut;

        [TestFixtureSetUp]
        public void fixture_setup()
        {
            run_action<before_all_observations>();
        }

        [SetUp]
        public void setup()
        {
            behaviour_performed_in_because = null;
            exception_thrown_while_the_sut_performed_its_work = null;
            dependencies = new Dictionary<Type, object>();
            prepare_to_make_an_observation();
        }


        [TearDown]
        public void tear_down()
        {
            run_action<after_each_observation>();
            UnitTestContainer.tear_down();
        }

        [TestFixtureTearDown]
        public void fixture_tear_down()
        {
            run_action<after_all_observations>();
        }

        void prepare_to_make_an_observation()
        {
            run_action<context>();
            sut = create_sut();
            run_action<after_the_sut_has_been_created>();
            run_action<because>();
        }

        after_each_observation a = () => dependencies.Clear();

        public virtual SUT create_sut()
        {
            return default(SUT);
        }

        ICommand build_command_chain<DelegateType>()
        {
            var actions = new Stack<ICommand>();
            var current_class = GetType();

            while (current_class.is_a_concern_type())
            {
                actions.Push(new DelegateFieldInvocation(typeof (DelegateType), this, current_class));
                current_class = current_class.BaseType;
            }

            return actions.as_command_chain();
        }

        void run_action<DelegateType>()
        {
            build_command_chain<DelegateType>().run();
        }


        static public void doing(Action because_behaviour)
        {
            behaviour_performed_in_because = because_behaviour;
        }

        static public Exception exception_thrown_by_the_sut
        {
            get { return exception_thrown_while_the_sut_performed_its_work ?? (exception_thrown_while_the_sut_performed_its_work = get_exception_throw_by(behaviour_performed_in_because)); }
        }

        static Exception get_exception_throw_by(Action because_behaviour)
        {
            try
            {
                because_behaviour();
                return null;
            }
            catch (Exception e)
            {
                return e;
            }
        }

        static public InterfaceType container_dependency<InterfaceType>() where InterfaceType : class
        {
            return container_dependency(an<InterfaceType>());
        }

        static public InterfaceType container_dependency<InterfaceType>(InterfaceType instance) where InterfaceType : class
        {
            UnitTestContainer.add_implementation_of(instance);
            return instance;
        }

        static public object an(Type type)
        {
            return MockRepository.GenerateStub(type, new object[0]);
        }

        static public InterfaceType an<InterfaceType>() where InterfaceType : class
        {
            return MockRepository.GenerateStub<InterfaceType>();
        }
    }
}

static public class ConcernExtensions
{
    static public bool is_a_concern_type(this Type type)
    {
        return typeof (IObservations)
            .IsAssignableFrom(type);
    }
}