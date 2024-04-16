using Scorewarrior.Test.Commands;

namespace Scorewarrior.Test
{
	public interface IExecutionDirector
	{
		void RegisterExecutor<TCommand, TExecutor>(TExecutor executor)
				where TCommand : class, ICommand
				where TExecutor : class, IExecutor<TCommand>;

		void Execute(ICommand command);
	}
}