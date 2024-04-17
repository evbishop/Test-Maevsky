using System;
using System.Collections.Generic;
using Scorewarrior.Test.Commands;

namespace Scorewarrior.Test
{
	public class ExecutionDirectorNoReflection : IExecutionDirector
	{
		private readonly Dictionary<Type, IExecutor> _executorByExecutableType;

		public ExecutionDirectorNoReflection()
		{
			_executorByExecutableType = new();
		}

        void IExecutionDirector.RegisterExecutor<TCommand, TExecutor>(TExecutor executor)
        {
            if (executor is IExecutor executorNoReflection)
                RegisterExecutor<TCommand>(executorNoReflection);
            else
                throw new Exception("Old executors cannot be used by the new execution director");
        }

		public void RegisterExecutor<TCommand>(IExecutor executor)
				where TCommand : class, ICommand
		{
			Type executableType = typeof(TCommand);
			if (_executorByExecutableType.ContainsKey(executableType))
			{
				throw new ArgumentException("Executor already registered");
			}
			_executorByExecutableType.Add(executableType, executor);
		}

		public void Execute(ICommand command)
		{
			Type executableType = command.GetType();
			if (_executorByExecutableType.TryGetValue(executableType, out IExecutor executor))
			{
                executor.Execute(command);
			}
			else
			{
				throw new Exception("Executor not found");
			}
		}
    }
}