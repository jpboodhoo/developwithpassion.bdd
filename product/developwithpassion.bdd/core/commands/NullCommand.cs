namespace developwithpassion.bdd.core.commands
{
    public class NullCommand : ICommand
    {
        public void run()
        {
        }
    }

    public class NullParameterizedCommand<T> : IParameterizedCommand<T> {
        public void run_against(T item)
        {
        }
    }
}