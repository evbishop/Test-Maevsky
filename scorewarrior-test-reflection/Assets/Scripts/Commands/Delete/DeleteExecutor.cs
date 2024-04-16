using UnityEngine;

namespace Scorewarrior.Test.Commands.Delete
{
	internal class DeleteExecutor : IExecutor<DeleteCommand>
	{
		public void Execute(DeleteCommand deleteCommand)
		{
			Debug.Log($"Delete with id: {deleteCommand.Id.ToString()}");
		}
	}
}