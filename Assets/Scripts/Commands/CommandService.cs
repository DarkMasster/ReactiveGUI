using System.Collections.Generic;

namespace ABC.Example.Commands
{
	public class CommandService : ICommandService
	{
		#region Private Fields
		private readonly List<ICommandProcessor> _processors = new List<ICommandProcessor>();
		#endregion

		#region Public Members
		public void Execute(ICommand command)
		{
			foreach (var processor in _processors)
			{
				if (processor.IsCanBeProcessed(command) == false)
				{
					continue;
				}

				processor.Execute(command);
			}
		}

		public void RegistryProcessor(ICommandProcessor processor)
		{
			processor.Init();
			_processors.Add(processor);
		}

		public void Stop(ICommand command)
		{
			foreach (var processor in _processors)
			{
				if (processor.IsCanBeProcessed(command) == false)
				{
					continue;
				}

				processor.Stop(command);
			}
		}
		#endregion
	}
}