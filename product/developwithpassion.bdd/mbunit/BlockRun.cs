using System;
using developwithpassion.bdd.core.commands;
using MbUnit.Core.Invokers;
using MbUnit.Core.Runs;

namespace developwithpassion.bdd.mbunit
{
    public class BlockRun<DelegateType> : Run
    {
        IParameterizedCommand<TestTreeArgs<DelegateType>> block_action;
        IRun main_run;

        public BlockRun(bool is_test, IParameterizedCommand<TestTreeArgs<DelegateType>> block_action,IRun main_run) : base("block", is_test)
        {
            this.block_action = block_action;
            this.main_run = main_run;
        }

        public override void Reflect(RunInvokerTree tree, RunInvokerVertex parent, Type t)
        {
            block_action.run_against(new TestTreeArgs<DelegateType>(tree, parent, t,main_run));
        }
    }
}