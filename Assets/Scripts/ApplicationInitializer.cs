using DM.NotifierTypes;
using UnityEngine;

public class ApplicationInitializer : MonoBehaviour
{
	#region Private Fields
	private PresenterMainWindow _presenterMainWindow;
	private IDataService _dataService;
	#endregion

	#region UnityMembers
	private void Start()
	{
		_dataService = new LocalDataService();
		
		var guiViewMainWindow = FindObjectOfType<GUIViewMainWindow>();
		var viewModelMainWindow = new ViewModelMainWindow();

		_presenterMainWindow = new PresenterMainWindow(_dataService, guiViewMainWindow, viewModelMainWindow);
		_presenterMainWindow.Init().Forget();
	}
	#endregion
}