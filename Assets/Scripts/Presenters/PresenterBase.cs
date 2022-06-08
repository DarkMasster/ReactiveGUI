public abstract class PresenterBase<TView, TModel> where TView : GUIViewBase<TModel>
{
	#region Protected Fields
	protected TView View;
	protected TModel ViewModel;
	#endregion

	#region Constructors
	protected PresenterBase(TView view, TModel model)
	{
		View = view;
		ViewModel = model;
		View.Init(ViewModel);
	}
	#endregion
}