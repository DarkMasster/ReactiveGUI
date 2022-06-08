using DM.NotifierTypes;

public class ViewModelPlayer
{
	#region Public Fields
	public NotifierProperty<float> CurHP = new NotifierProperty<float>();
	public NotifierProperty<float> MaxHP = new NotifierProperty<float>();
	public NotifierProperty<string> Name = new NotifierProperty<string>();
	public NotifierProperty<float> Speed = new NotifierProperty<float>();
	#endregion
}

public class ViewModelMainWindow
{
	#region Public Fields
	public NotifierProperty<float> InitProgress = new NotifierProperty<float>();
	public NotifierProperty<ViewModelPlayer> CurrentPlayer = new NotifierProperty<ViewModelPlayer>();
	public NotifierDictionary<string, ViewModelPlayer> Players = new NotifierDictionary<string, ViewModelPlayer>();
	#endregion
}