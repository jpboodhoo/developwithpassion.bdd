using System;
using MbUnit.Core.Invokers;
using MbUnit.Core.Runs;

namespace developwithpassion.bdd.mbunit
{
    public class TestTreeArgs<DelegateType>
    {
        public RunInvokerTree tree { get; private set; }
        public RunInvokerVertex parent { get; private set; }
        public Type type_that_contains_tests { get; private set; }
        public IRun run{ get; private set;}

        public TestTreeArgs(RunInvokerTree tree, RunInvokerVertex parent, Type type_that_contains_tests,IRun run)
        {
            this.tree = tree;
            this.parent = parent;
            this.type_that_contains_tests = type_that_contains_tests;
            this.run = run;
        }
    }
}