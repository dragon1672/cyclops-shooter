using System;
using UnityEngine;
using System.Collections;

public delegate void VoidAction(Collision c);

[RequireComponent(typeof(Collider))]
public class ColliderNotify : MonoBehaviour
{
	public VoidAction OnCollidionAction = null;
	void Start()
	{
		GetComponent<Collider>().isTrigger = true;
	}

	void OnCollisionEnter(Collision collision)
	{
		if (OnCollidionAction != null) OnCollidionAction(collision);
	}
}
