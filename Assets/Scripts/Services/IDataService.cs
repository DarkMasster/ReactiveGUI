using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

public interface IDataService
{
	#region Properties
	List<PlayerData> Players { get; }
	#endregion

	#region Public Members
	UniTask<PlayerData> CreatePlayerData();
	UniTask LoadPlayerData(IProgress<float> progress);
	#endregion
}