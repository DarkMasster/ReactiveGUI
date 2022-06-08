using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Random = System.Random;

public class LocalDataService : IDataService
{
	#region Private Fields
	private readonly string[] Names = {"Adam", "Beta", "Vasiliy", "Aragorn", "Nagibator 999", "Masha", "SupSup", "Gacha"};
	#endregion

	#region Properties
	public List<PlayerData> Players { get; }
	#endregion

	#region Constructors
	public LocalDataService()
	{
		Players = new List<PlayerData>();
	}
	#endregion

	#region Public Members
	public async UniTask<PlayerData> CreatePlayerData()
	{
		var maxHp = new Random().Next(50, 200);
		var curHp = new Random().Next(0, maxHp);

		var name = Names[new Random().Next(0, Names.Length)];

		if (Players.Find(item => item.Name == name) != null)
		{
			name += " " + Time.realtimeSinceStartup;
		}

		var playerData = new PlayerData
		{
			Name = name,
			Speed = new Random().Next(2, 22),
			MaxHP = maxHp
		};

		Players.Add(playerData);

		return playerData;
	}

	public async UniTask LoadPlayerData(IProgress<float> progress)
	{
		var loadingMaxIterations = 500f;
		
		progress.Report(0);
		for (var i = 0; i < loadingMaxIterations; i++)
		{
			await UniTask.Delay(1);
			progress.Report(i/loadingMaxIterations);
		}

		CreateDefaultPlayer();
		progress.Report(1);
	}
	#endregion

	#region Private Members
	private void CreateDefaultPlayer()
	{
		var playerData = new PlayerData
		{
			Name = "DefaultPlayer",
			Speed = 10f,
			MaxHP = 100f
		};

		Players.Add(playerData);
	}
	#endregion
}