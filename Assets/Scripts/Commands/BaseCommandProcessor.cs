using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace ABC.Example.Commands
{
	public interface ICommandProcessor
	{
		#region Public Members
		void Execute(ICommand command);
		void Init();
		bool IsCanBeProcessed(ICommand command);
		bool IsExecuting(ICommand command);
		void Stop(ICommand command);
		void UnInit();
		void UnregisterAll();
		#endregion
	}

	public abstract class BaseCommandProcessor<T> : ICommandProcessor where T : class, ICommand
	{
		#region Protected Fields
		protected Dictionary<ICommand, CancellationTokenSource> _cancellationTokenCommands = new Dictionary<ICommand, CancellationTokenSource>();
		protected Dictionary<ICommand, UniTask> _executingCommands = new Dictionary<ICommand, UniTask>();
		#endregion

		#region Public Members
		public virtual void Execute(ICommand command)
		{
			var cts = new CancellationTokenSource();
			_cancellationTokenCommands.Add(command, cts);
			_executingCommands.Add(command, Process(command, cts.Token));
		}

		public T GetTypedCommand(ICommand command)
		{
			return command as T;
		}

		public virtual void Init()
		{
		}

		public virtual bool IsCanBeProcessed(ICommand command)
		{
			return command is T;
		}

		public virtual bool IsExecuting(ICommand command)
		{
			return _executingCommands.ContainsKey(command) && !_executingCommands[command].Status.IsCompleted() && !_executingCommands[command].Status.IsCompletedSuccessfully();
		}

		public virtual void Stop(ICommand command)
		{
			if (IsExecuting(command))
			{
				_cancellationTokenCommands[command].Cancel();
				_executingCommands.Remove(command);
			}
		}

		public virtual void UnInit()
		{
			UnregisterAll();
		}

		public void UnregisterAll()
		{
		}
		#endregion

		#region Protected Members
		protected abstract UniTask Process(ICommand command, CancellationToken cts);
		#endregion
	}
}