﻿using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class FirebaseController : MonoBehaviour {
	private IFirebase firebaseObject,positionRef, positionRefx, positionRefy, positionRefz;
	private volatile float positionx, positiony, positionz;
	private bool initialized = false;
	private Queue<RPCItem> invokeQueue = new Queue<RPCItem>();
	private object sync = new object ();
	private Dictionary<string, IFirebase> firebases = new Dictionary<string, IFirebase> ();

	public string path;
	public bool isNPC;

	struct RPCItem {
		public Action<string> Method;
		public string Value;
	}

	void Start() {
		Initialize ();
	}

	public void SetPath(string path) {
		this.path = path;
		Initialize ();
	}

	public void RemoteCall(string objectName, string value) {
		if (firebaseObject != null) {
			IFirebase child;
			lock(sync) {
				if (!firebases.TryGetValue(objectName, out child)) {
					child = firebaseObject.Child(objectName); 
					firebases.Add(objectName, child);
				}
			}
			child.Push().SetValue(value);
		}
	}

	public void HandleCall(string objectName, Action<string> callback) {
		if (firebaseObject != null) {
			IFirebase child;
			lock(sync) {
				if (!firebases.TryGetValue(objectName, out child)) {
					child = firebaseObject.Child(objectName); 
					firebases.Add(objectName, child);
				}
			}

			child.ChildAdded += (object sender, ChangedEventArgs e) => {
				lock(sync) {
					invokeQueue.Enqueue(new RPCItem() { Method = callback, Value = e.DataSnapshot.GetStringValue()});
				}
			};
		}
	}

	void Initialize() {
		if (!initialized && !string.IsNullOrEmpty (path)) {
			initialized = true;
			firebaseObject = Firebase.CreateNew (path);
			if (firebaseObject != null) {
				FirebaseDebugLog.Initialize(path + "/DebugLog");
				positionRef = firebaseObject.Child ("Position");

				positionRefx = positionRef.Child ("x");
				positionRefy = positionRef.Child ("y");
				positionRefz = positionRef.Child ("z");

				if (isNPC) {
//					positionRef.ValueUpdated += (object sender, ChangedEventArgs e) => {
//						var dictionary = e.DataSnapshot.GetDictionaryValue();
//						positionx = Convert.ToSingle(dictionary["x"]);
//						positiony = Convert.ToSingle(dictionary["y"]);
//						positionz = Convert.ToSingle(dictionary["z"]);
//					};
					positionRefx.ValueUpdated += (object sender, ChangedEventArgs e) => positionx = e.DataSnapshot.GetFloatValue ();
					positionRefy.ValueUpdated += (object sender, ChangedEventArgs e) => positiony = e.DataSnapshot.GetFloatValue ();
					positionRefz.ValueUpdated += (object sender, ChangedEventArgs e) => positionz = e.DataSnapshot.GetFloatValue ();
				}
			}
		}
	}

	void FixedUpdate ()
	{
		RPCItem[] rpcItems = null;

		lock (sync) {
			rpcItems = new RPCItem[invokeQueue.Count];
			invokeQueue.CopyTo(rpcItems, 0);
			invokeQueue.Clear();
		}
		foreach (RPCItem item in rpcItems) {
			item.Method(item.Value);
		}

		if (positionRefx != null && positionRefy != null && positionRefz != null) {
			if (isNPC) {
				GetComponent<Rigidbody> ().position = new Vector3 
				(
					-positionx,  
					0.0f, 
					(5f - positionz + 5)
				);
			}
			else {
				positionRefx.SetValue (GetComponent<Rigidbody> ().position.x);
				positionRefy.SetValue (GetComponent<Rigidbody> ().position.y);
				positionRefz.SetValue (GetComponent<Rigidbody> ().position.z);
			}
		}
	}
}
