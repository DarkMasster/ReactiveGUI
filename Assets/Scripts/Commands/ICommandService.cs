namespace ABC.Example.Commands
{
	public interface ICommandService
	{
		#region Public Members
		void Execute(ICommand command);
		void RegistryProcessor(ICommandProcessor processor);
		void Stop(ICommand command);
		#endregion
	}
}