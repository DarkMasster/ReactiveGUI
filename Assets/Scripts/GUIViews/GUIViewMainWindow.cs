using System;
using System.Linq;
using DM.NotifierTypes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GUIViewMainWindow : GUIViewBase<ViewModelMainWindow>
{
	#region UnitySerialized
	[Header("Players List")]
	[SerializeField] private GameObject _contentPlayes;
	[SerializeField] private GameObject _contentPlayesItemPrefab;
	[SerializeField] private ToggleGroup _contentPlayesToggleGroup;

	[Header("Player Info")]
	[SerializeField] private GameObject _attributesRoot;
	[SerializeField] private TextMeshProUGUI _labelName;
	[SerializeField] private TextMeshProUGUI _labelSpeed;
	[SerializeField] private Slider _sliderHP;

	[Header("Loading")] 
	[SerializeField] private GameObject _loadingRoot;
	[SerializeField] private Slider _sliderLoading;

	[Header("Controls")] 
	[SerializeField] private Button _buttonAddNewPlayer;
	#endregion

	#region Events
	public event Action OnButtonAddNewPlayerClick;
	public event Action<string> OnDeletePlayer;
	public event Action<string> OnSelectPlayer;
	#endregion

	#region EventHandlers
	private void HandleCurHpValueChanged(object sender, GenericEventArg<float> arg)
	{
		_sliderHP.value = arg.Value;
	}

	private void HandleCurrentPlayerValueChanged(object sender, PropertyEventArgs<ViewModelPlayer> propertyEventArgs)
	{
		if (propertyEventArgs.OldValue != null && propertyEventArgs.NewValue != null)
		{
			propertyEventArgs.NewValue.Name.OnValueChanged -= HandleNameValueChanged;
			propertyEventArgs.NewValue.Speed.OnValueChanged -= HandleSpeedValueChanged;
			propertyEventArgs.NewValue.MaxHP.OnValueChanged -= HandleMaxHpValueChanged;
			propertyEventArgs.NewValue.CurHP.OnValueChanged -= HandleCurHpValueChanged;
		}

		if (propertyEventArgs.NewValue != null)
		{
			SetEnableAttributes(true);

			_labelName.text = propertyEventArgs.NewValue.Name;
			_labelSpeed.text = propertyEventArgs.NewValue.Speed.ToString();
			_sliderHP.maxValue = propertyEventArgs.NewValue.MaxHP;
			_sliderHP.value = propertyEventArgs.NewValue.CurHP;

			propertyEventArgs.NewValue.Name.OnValueChanged += HandleNameValueChanged;
			propertyEventArgs.NewValue.Speed.OnValueChanged += HandleSpeedValueChanged;
			propertyEventArgs.NewValue.MaxHP.OnValueChanged += HandleMaxHpValueChanged;
			propertyEventArgs.NewValue.CurHP.OnValueChanged += HandleCurHpValueChanged;
		}
		else
		{
			SetEnableAttributes(false);
		}
	}

	private void HandleMaxHpValueChanged(object sender, GenericEventArg<float> arg)
	{
		_sliderHP.maxValue = arg.Value;
	}

	private void HandleNameValueChanged(object sender, GenericEventArg<string> arg)
	{
		_labelName.text = arg.Value;
	}

	private void HandleOnInitProgressChanged(object sender, GenericEventArg<float> args)
	{
		if (args.Value == 0)
		{
			_sliderLoading.maxValue = 1;
			_sliderLoading.wholeNumbers = false;
			_loadingRoot.gameObject.SetActive(true);
		}
		else if (args.Value == -1f || args.Value == 1f)
		{
			_loadingRoot.gameObject.SetActive(false);
		}

		_sliderLoading.value = args.Value;
	}

	private void HandlePlayersAddItem(object sender, GenericPairEventArgs<string, ViewModelPlayer> args)
	{
		var go = Instantiate(_contentPlayesItemPrefab, _contentPlayes.transform);
		go.transform.Find("Label").GetComponent<TextMeshProUGUI>().text = args.Key;
		var toggle = go.GetComponent<Toggle>();
		toggle.group = _contentPlayesToggleGroup;
		toggle.onValueChanged.AddListener(HandleToggleValueChanged);
		go.transform.Find("Button").GetComponent<Button>().onClick.AddListener(HandlePlayerDeleteButtonClick);
	}

	private void HandleSpeedValueChanged(object sender, GenericEventArg<float> arg)
	{
		_labelSpeed.text = arg.Value.ToString();
	}
	#endregion

	#region Private Members
	private void HandleButtonAddNewPlayerClick()
	{
		OnButtonAddNewPlayerClick?.Invoke();
	}

	private void HandlePlayerDeleteButtonClick()
	{
		var selectedToggle = _contentPlayesToggleGroup.ActiveToggles().FirstOrDefault();
		if (selectedToggle != null)
		{
			var id = selectedToggle.transform.Find("Label").GetComponent<TextMeshProUGUI>().text;
			if (Model.CurrentPlayer != null && Model.CurrentPlayer.Value !=null && Model.CurrentPlayer.Value.Name == id)
			{
				Model.CurrentPlayer.Value = null;
			}
			Destroy(selectedToggle.gameObject);
			Model.Players.Remove(id);
			OnDeletePlayer?.Invoke(id);
		}
	}

	private void HandleToggleValueChanged(bool value)
	{
		if (value)
		{
			var selectedToggle = _contentPlayesToggleGroup.ActiveToggles().FirstOrDefault();

			if (selectedToggle != null)
			{
				OnSelectPlayer?.Invoke(selectedToggle.transform.Find("Label").GetComponent<TextMeshProUGUI>().text);
			}
		}
	}

	private void SetEnableAttributes(bool value)
	{
		_attributesRoot.gameObject.SetActive(value);
	}
	#endregion

	#region Overrides
	protected override void CustomInit()
	{
		Model.Players.OnAddItem += HandlePlayersAddItem;
		Model.CurrentPlayer.OnValueChangedExtended += HandleCurrentPlayerValueChanged;
		Model.InitProgress.OnValueChanged += HandleOnInitProgressChanged;

		_buttonAddNewPlayer.onClick.AddListener(HandleButtonAddNewPlayerClick);
	}

	protected override void CustomUnInit()
	{
		Model.Players.OnAddItem -= HandlePlayersAddItem;
		Model.CurrentPlayer.OnValueChangedExtended += HandleCurrentPlayerValueChanged;
		Model.InitProgress.OnValueChanged -= HandleOnInitProgressChanged;

		_buttonAddNewPlayer.onClick.RemoveAllListeners();
	}
	#endregion
}