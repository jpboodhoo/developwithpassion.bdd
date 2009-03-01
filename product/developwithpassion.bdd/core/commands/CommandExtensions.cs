using System.Collections.Generic;
using developwithpassion.bdd.core.extensions;

namespace developwithpassion.bdd.core.commands
{
    static public class CommandExtensions
    {
        static public ICommand followed_by(this ICommand first, ICommand second)
        {
            return new ChainedCommand(first, second);
        }

        static public IParameterizedCommand<T> followed_by<T>(this IParameterizedCommand<T> first, IParameterizedCommand<T> second)
        {
            return new ChainedParameterizedCommand<T>(first, second);
        }

        static public ICommand as_command_chain(this IEnumerable<ICommand> commands){
            ICommand chain = new NullCommand();
            commands.each(x => chain = chain.followed_by(x));
            return chain;
        }

        static public IParameterizedCommand<T> as_command_chain<T>(this IEnumerable<IParameterizedCommand<T>> commands){
            IParameterizedCommand<T> chain = new NullParameterizedCommand<T>();
            commands.each(x => chain = chain.followed_by(x));
            return chain;
        }
    }
}