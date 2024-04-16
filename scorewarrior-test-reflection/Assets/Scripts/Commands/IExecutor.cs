namespace Scorewarrior.Test.Commands
{
	public interface IExecutor<in TCommand> where TCommand : class, ICommand
	{
		void Execute(TCommand executable);
	}
}