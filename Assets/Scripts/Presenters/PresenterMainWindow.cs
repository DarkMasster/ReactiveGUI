using Cysharp.Threading.Tasks;
using DM.NotifierTypes;
using UnityEngine;

public class PresenterMainWindow : PresenterBase<GUIViewMainWindow, ViewModelMainWindow>
{
	#region Private Fields
	private UniTask _currentTask;
	private readonly IDataService _dataService;
	#endregion

	#region Constructors
	public PresenterMainWindow(IDataService dataService, GUIViewMainWindow view, ViewModelMainWindow model) : base(view, model)
	{
		_dataService = dataService;
	}
	#endregion

	#region EventHandlers
	private void HandleOnButtonAddNewPlayerClick()
	{
		var playerData = _dataService.CreatePlayerData().GetAwaiter().GetResult();
		CreatePlayerViewModel(playerData);
	}

	private void HandleOnDeletePlayer(string id)
	{
		var forRemove = _dataService.Players.Find(item => item.Name == id);

		if (forRemove != null)
		{
			_dataService.Players.Remove(forRemove);
		}
	}
	private void HandleOnSelectPlayer(string id)
	{
		ViewModel.CurrentPlayer.Value = ViewModel.Players[id];

		if (_currentTask.Status != UniTaskStatus.Pending)
		{
			_currentTask = RandomHp();
		}
	}
	#endregion

	#region Public Members
	public async UniTaskVoid Init()
	{
		ViewModel.CurrentPlayer.Value = null;
		View.OnButtonAddNewPlayerClick += HandleOnButtonAddNewPlayerClick;
		View.OnSelectPlayer += HandleOnSelectPlayer;
		View.OnDeletePlayer += HandleOnDeletePlayer;
		ViewModel.InitProgress.Value = -1f;

		var progress = Progress.Create<float>(HandleInitProgress);
		await _dataService.LoadPlayerData(progress);

		foreach (var playerData in _dataService.Players)
		{
			CreatePlayerViewModel(playerData);
		}
	}
	#endregion

	#region Private Members
	private void CreatePlayerViewModel(PlayerData playerData)
	{
		var playerViewModel = new NotifierProperty<ViewModelPlayer>
		{
			Value = new ViewModelPlayer
			{
				Name = new NotifierProperty<string>(playerData.Name),
				Speed = new NotifierProperty<float>(playerData.Speed),
				CurHP = new NotifierProperty<float>(playerData.MaxHP),
				MaxHP = new NotifierProperty<float>(playerData.MaxHP)
			}
		};

		ViewModel.Players.Add(playerData.Name, playerViewModel);
	}

	private void HandleInitProgress(float value)
	{
		ViewModel.InitProgress.Value = value;
	}

	private async UniTask RandomHp()
	{
		for (var i = 0; i < 10; i++)
		{
			ViewModel.CurrentPlayer.Value.CurHP.Value = Random.Range(0, ViewModel.CurrentPlayer.Value.MaxHP.Value);
			await UniTask.Delay(20);
		}
	}
	#endregion
}