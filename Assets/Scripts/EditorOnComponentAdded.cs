using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[InitializeOnLoad]
public class EditorOnComponentAdded
{
/*
	static EditorOnComponentAdded()
	{
		ObjectFactory.componentWasAdded -= HandleComponentAdded;
		ObjectFactory.componentWasAdded += HandleComponentAdded;
 
		EditorApplication.quitting -= OnEditorQuiting;
		EditorApplication.quitting += OnEditorQuiting;
	}
	private static void HandleComponentAdded(UnityEngine.Component obj)
	{
		if (obj is Animator && obj.GetComponent<AnimatorEventsReceiver>() == null)
		{
			obj.gameObject.AddComponent<AnimatorEventsReceiver>();
		}
	}
 
	private static void OnEditorQuiting()
	{
		ObjectFactory.componentWasAdded -= HandleComponentAdded;
		EditorApplication.quitting -= OnEditorQuiting;
	}
*/
}