#if (UNITY_EDITOR || UNITY_STANDALONE)
internal class QueryStandaloneImpl : IQuery
{
	private EventHandler<ChangedEventArgs> valueUpdatedEvent;
	private EventHandler<ChangedEventArgs> childAddedEvent;
	private ValueEventListener valueUpdatedListener;
	private ChildEventListener childAddedListener;

	protected QueryStandaloneImpl ()
	{}
	
	public event EventHandler<ChangedEventArgs> ValueUpdated
	{
		add
		{
			valueUpdatedEvent += value;
			if (valueUpdatedListener == null)
			{
				valueUpdatedListener = new ValueEventListener (this);
			}
		}
		remove
		{
			valueUpdatedEvent -= value;
			if (valueUpdatedEvent == null)
			{
				valueUpdatedListener = null;
			}
		}
	}

	public event EventHandler<ChangedEventArgs> ChildAdded
	{
		add
		{
			childAddedEvent += value;
			if (childAddedListener == null)
			{
				childAddedListener = new ChildEventListener (this);
			}
		}
		remove
		{
			childAddedEvent -= value;
			if (childAddedEvent == null)
			{
				childAddedListener = null;
			}
		}
	}
	
	public event EventHandler<ErrorEventArgs> Error;

	private void OnValueUpdated (DataSnapshotAndroidImpl snapshot)
	{
		EventHandler<ChangedEventArgs> handler = valueUpdatedEvent;
		if (handler != null)
		{
			handler(this, new ChangedEventArgs () { DataSnapshot = snapshot });
		}
	}

	private void OnChildAdded (DataSnapshotAndroidImpl snapshot)
	{
		EventHandler<ChangedEventArgs> handler = childAddedEvent;
		if (handler != null)
		{
			handler(this, new ChangedEventArgs () { DataSnapshot = snapshot });
		}
	}
}
#endif
