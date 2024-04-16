namespace Scorewarrior.Test.Commands.Delete
{
	internal class DeleteCommand : ICommand
	{
		public readonly uint Id;

		public DeleteCommand(uint id)
		{
			Id = id;
		}
	}
}