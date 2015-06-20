#if (UNITY_EDITOR || UNITY_STANDALONE)
internal class FirebaseErrorStandaloneImpl : IFirebaseError
{
	public int GetCode ()
	{
		throw new System.NotImplementedException ();
	}

	public string GetMessage ()
	{
		throw new System.NotImplementedException ();
	}

	public string GetDetails ()
	{
		throw new System.NotImplementedException ();
	}
}
#endif
