namespace developwithpassion.bdd.core.commands
{
    public class ChainedParameterizedCommand<T> : IParameterizedCommand<T>
    {
        IParameterizedCommand<T> first;
        IParameterizedCommand<T> second;
        public ChainedParameterizedCommand(IParameterizedCommand<T> first, IParameterizedCommand<T> second)
        {
            this.first = first;
            this.second = second;
        }

        public void run_against(T item)
        {
            first.run_against(item);
            second.run_against(item);
        }
    }
}