using System.Collections;
using developwithpassion.bdd.core.commands;
using MbUnit.Core.Invokers;
using MbUnit.Core.Runs;

namespace developwithpassion.bdd.mbunit
{
    public class TestDelegateInvoker<DelegateType> : RunInvoker
    {
        IParameterizedCommand<object> block_command;

        public TestDelegateInvoker(IRun generator, IParameterizedCommand<object> block_command) : base(generator)
        {
            this.block_command = block_command;
        }

        public override object Execute(object o, IList args)
        {
            block_command.run_against(o);
            return new object();
        }

        public override string Name
        {
            get { return string.Empty; }
        }
    }
}