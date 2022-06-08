using System;

public class NotifierListSortingArgs< TValue > : EventArgs
{
	#region Public Fields
	public int OldIndex;
	public int NewIndex;
	public TValue Value;
	#endregion

	#region Constructors
	public NotifierListSortingArgs()
	{
	}

	public NotifierListSortingArgs( int oldIndex, int newIndex, TValue value )
	{
		OldIndex = oldIndex;
		NewIndex = newIndex;
		Value = value;
	}
	#endregion
}
