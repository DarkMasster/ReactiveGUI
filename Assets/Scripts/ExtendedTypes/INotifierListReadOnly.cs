using System;
using System.Collections.Generic;

namespace DM.NotifierTypes
{
	public interface INotifierListReadOnly< T > : IEnumerable< T >
	{
		#region Properties
		int Count { get; }
		T this[ int index ] { get; }
		#endregion

		#region Public Members
		int IndexOf( T item );

		event EventHandler< GenericPairEventArgs< int, T > > OnAddItem;
		event EventHandler< GenericEventArg< IEnumerable< T > > > OnAddRange;
		event EventHandler< GenericEventArg< IEnumerable< T > > > OnClear;
		event EventHandler< GenericPairEventArgs< int, T > > OnElementChange;
		event EventHandler< GenericPairEventArgs< int, T > > OnRemoveItem;
		#endregion
	}
}