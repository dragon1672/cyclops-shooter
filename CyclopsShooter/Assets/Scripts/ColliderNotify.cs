using System;
using UnityEngine;
using System.Collections;

public delegate void VoidAction(Collision c);

public class ColliderNotify : MonoBehaviour
{
	public VoidAction OnCollidionAction = null;
	void OnCollisionEnter(Collision collision)
	{
		if (OnCollidionAction != null) OnCollidionAction(collision);
	}
}
