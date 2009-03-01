namespace developwithpassion.bdd.core.commands
{
    public interface IParameterizedCommand<T>
    {
        void run_against(T item);
    }
}