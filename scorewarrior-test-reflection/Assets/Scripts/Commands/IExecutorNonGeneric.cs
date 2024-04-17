namespace Scorewarrior.Test.Commands
{
	public interface IExecutor
	{
		void Execute(ICommand executable);
	}
}