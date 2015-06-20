#if (UNITY_EDITOR || UNITY_STANDALONE)
internal class FirebaseStandaloneImpl : QueryStandaloneImpl, IFirebase
{
	static bool initialized = false;
	
	public FirebaseStandaloneImpl (string path)
	{
	}

	public IFirebase Child (string name)
	{
		return null;
	}
	
	public IFirebase GetParent ()
	{
		return null;
	}
	
	public IFirebase GetRoot ()
	{
		return null;
	}
	
	public string GetKey ()
	{
		return null;
	}
	
	public IFirebase Push ()
	{
		return null;
	}
	
	public void SetValue (string value)
	{
	}
	
	public void SetValue (string value, string priority, Action<IFirebaseError, IFirebase> listener)
	{
	}
	
	public void SetValue (float value)
	{
	}
	
	public void SetValue (float value, string priority, Action<IFirebaseError, IFirebase> listener)
	{
	}
	
	public void SetPriority (string priority)
	{
	}
	
	public void SetPriority (string priority, Action<IFirebaseError, IFirebase> listener)
	{
	}

	public class Factory : IFirebaseFactory
	{
		#region IFirebaseFactory implementation
		public IFirebase TryCreate (string path)
		{
			if (Application.platform == RuntimePlatform.WindowsEditor ||
			    Application.platform == RuntimePlatform.WindowsPlayer ||
			    Application.platform == RuntimePlatform.OSXEditor ||
			    Application.platform == RuntimePlatform.OSXPlayer)
			{
				return new FirebaseStandaloneImpl (path);
			}
			return null;
		}
		#endregion
	}
}
#endif
