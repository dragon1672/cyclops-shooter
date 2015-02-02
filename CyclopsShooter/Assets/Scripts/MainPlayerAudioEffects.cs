using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CyclopsMainPlayer))]
public class MainPlayerAudioEffects : MonoBehaviour
{
	private CyclopsMainPlayer player;
	// Use this for initialization
	void Start ()
	{
		player = GetComponent<CyclopsMainPlayer>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
