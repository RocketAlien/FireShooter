using System;

#if (UNITY_EDITOR || UNITY_STANDALONE)
internal class QueryStandaloneImpl : IQuery
{
	private EventHandler<ChangedEventArgs> valueUpdatedEvent;
	private EventHandler<ChangedEventArgs> childAddedEvent;
	//private ValueEventListener valueUpdatedListener;
	//private ChildEventListener childAddedListener;

	protected QueryStandaloneImpl ()
	{}
	
	public event EventHandler<ChangedEventArgs> ValueUpdated
	{
		add
		{
			valueUpdatedEvent += value;
		}
		remove
		{
			valueUpdatedEvent -= value;
		}
	}

	public event EventHandler<ChangedEventArgs> ChildAdded
	{
		add
		{
			childAddedEvent += value;
		}
		remove
		{
			childAddedEvent -= value;
		}
	}
	
	public event EventHandler<ErrorEventArgs> Error;

	private void OnValueUpdated (DataSnapshotStandaloneImpl snapshot)
	{
		EventHandler<ChangedEventArgs> handler = valueUpdatedEvent;
		if (handler != null)
		{
			handler (this, new ChangedEventArgs () { DataSnapshot = snapshot });
		}
	}

	private void OnChildAdded (DataSnapshotStandaloneImpl snapshot)
	{
		EventHandler<ChangedEventArgs> handler = childAddedEvent;
		if (handler != null)
		{
			handler (this, new ChangedEventArgs () { DataSnapshot = snapshot });
		}
	}
}
#endif
