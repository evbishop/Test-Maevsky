using System;
using UnityEngine;

namespace Scorewarrior.Test.Commands.Push
{
	internal class PushExecutorNoReflection : IExecutor, IExecutor<PushCommand>
	{
		public void Execute(ICommand command)
		{
            if (command is PushCommand pushCommand)
			    Debug.Log($"Push with name: {pushCommand.Name}");
            else
                throw new Exception("Invalid argument. Command must be a PushCommand");
		}

		public void Execute(PushCommand command)
        {
            Execute(command);
        }
	}
}