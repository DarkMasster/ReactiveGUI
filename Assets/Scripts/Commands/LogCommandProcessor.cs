using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace ABC.Example.Commands
{
	public class LogCommandProcessor : BaseCommandProcessor<LogCommand>
	{
		#region Ovverides
		protected override async UniTask Process(ICommand command, CancellationToken cancellationToken )
		{
			for (var  i = 0; i < 10; i++)
			{
				Debug.LogWarning(GetTypedCommand(command).Value);
				await UniTask.Delay( 1000, cancellationToken: cancellationToken );
			}
			
		}
		#endregion
	}
}