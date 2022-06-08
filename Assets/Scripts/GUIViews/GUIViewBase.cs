using UnityEngine;



namespace UnityCore.Aggregators
{
}


public abstract class GUIViewBase<TModel> : MonoBehaviour
{
	#region Properties
	public TModel Model { get; set; }
	#endregion

	#region Public Members
	public void Init( TModel model )
	{
		Model = model;
		CustomInit();
	}
	#endregion

	#region Protected Members
	protected virtual void CustomInit()
	{
	}

	protected virtual void CustomUnInit()
	{
	}
	#endregion

	#region UnityMembers
	private void OnDestroy()
	{
		CustomUnInit();
	}
	#endregion
}