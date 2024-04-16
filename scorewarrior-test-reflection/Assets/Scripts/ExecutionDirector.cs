using System;
using System.Collections.Generic;
using System.Reflection;
using Scorewarrior.Test.Commands;

namespace Scorewarrior.Test
{
	public class ExecutionDirector : IExecutionDirector
	{
		private readonly Dictionary<Type, object> _executorByExecutableType;

		public ExecutionDirector()
		{
			_executorByExecutableType = new Dictionary<Type, object>();
		}

		public void RegisterExecutor<TCommand, TExecutor>(TExecutor executor)
				where TCommand : class, ICommand
				where TExecutor : class, IExecutor<TCommand>
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
			if (_executorByExecutableType.TryGetValue(executableType, out object executor))
			{
				Type executorType = executor.GetType();
				MethodInfo executeMethod = executorType.GetMethod("Execute");
				if (executeMethod != null)
				{
					executeMethod.Invoke(executor, new object[]{command});
				}
				else
				{
					throw new Exception("Execute method not found");
				}
			}
			else
			{
				throw new Exception("Executor not found");
			}
		}
	}
}