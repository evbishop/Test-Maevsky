namespace Scorewarrior.Test.Commands.Push
{
	internal class PushCommand : ICommand
	{
		public readonly string Name;

		public PushCommand(string name)
		{
			Name = name;
		}
	}
}