namespace ABC.Example.Commands
{
	public class LogCommand : ICommand
	{
		#region Properties
		public string Value { get; set; }
		#endregion

		#region Constructors
		public LogCommand(string value)
		{
			Value = value;
		}
		#endregion
	}
}