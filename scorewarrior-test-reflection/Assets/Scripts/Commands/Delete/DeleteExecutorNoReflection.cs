using System;
using UnityEngine;

namespace Scorewarrior.Test.Commands.Delete
{
	internal class DeleteExecutorNoReflection : IExecutor, IExecutor<DeleteCommand>
	{
        public void Execute(ICommand command)
        {
            if (command is DeleteCommand deleteCommand)
                Debug.Log($"Delete with id: {deleteCommand.Id}");
            else
                throw new Exception("Invalid argument. Command must be a DeleteCommand");
        }

        public void Execute(DeleteCommand command)
        {
            Execute(command);
        }
    }
}