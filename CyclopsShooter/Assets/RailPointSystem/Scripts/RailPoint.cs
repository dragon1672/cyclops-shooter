using UnityEngine;
using System.Collections;

public delegate void VoidEvent();

public class RailPoint : MonoBehaviour
{
    public GameObject ToLookAtWhenInRouteToMe;
    public bool HasBeenReached { get; private set; }

	public VoidEvent EnterAction = null;
	public VoidEvent ExitAction = null;

	// Use this for initialization
	void Start () 
    {
        HasBeenReached = false; 
	}

    void OnTriggerEnter(Collider c)
    {
        HasBeenReached = true;
		if (EnterAction != null) EnterAction();
    }

    void OnTriggerExit(Collider c)
    {
	    HasBeenReached = false;
	    if (ExitAction != null) ExitAction();
    }
}
