using System;
using developwithpassion.bdd.contexts;
using MbUnit.Core.Framework;
using MbUnit.Core.Runs;
using MbUnit.Framework;

namespace developwithpassion.bdd.mbunit.standard
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ObservationsAttribute : TestFixturePatternAttribute
    {
        public ObservationsAttribute(string description) : base(description) {}

        public ObservationsAttribute() {}

        public override IRun GetRun()
        {
            var run = new SequenceRun();
            run.Runs.Add(new OptionalMethodRun(typeof (SetUpAttribute), false));
            run.Runs.Add(new MethodRun(typeof (ObservationAttribute), true, true));
            run.Runs.Add(new BlockRun<it>(true,new AppendTestsToTestTree(),run));
            run.Runs.Add(new OptionalMethodRun(typeof (TearDownAttribute), false));
            return run;
        }
    }
}