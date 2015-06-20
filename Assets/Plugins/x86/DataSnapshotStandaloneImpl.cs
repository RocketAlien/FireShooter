#if (UNITY_EDITOR || UNITY_STANDALONE)
internal class DataSnapshotStandaloneImpl : IDataSnapshot
{
	public DataSnapshotStandaloneImpl ()
	{
	}
	
	public IDataSnapshot Child (string path)
	{
		return null;
	}
	
	public bool Exists ()
	{
		return false;
	}
	
	public string GetKey ()
	{
		return null;
	}
	
	public object GetPriority ()
	{
		return null;
	}

	public IFirebase GetRef ()
	{
		return null;
	}
	
	public float GetFloatValue ()
	{
		return 0f;
	}
	
	public string GetStringValue ()
	{
		return null;
	}

	public Dictionary<string, object> GetDictionaryValue ()
	{
		return null;
	}

	public bool HasChild (string path)
	{
		return false;
	}
}
#endif
