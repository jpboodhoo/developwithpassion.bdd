namespace developwithpassion.bdd.core.commands
{
    public class ChainedCommand : ICommand
    {
        private readonly ICommand first_command;
        private readonly ICommand second_command;

        public ChainedCommand(ICommand first_command, ICommand second_command)
        {
            this.first_command = first_command;
            this.second_command = second_command;
        }

        public void run()
        {
            first_command.run();
            second_command.run();
        }
    }
}