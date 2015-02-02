using UnityEngine;
using System.Collections;

public class FakeSoldigerInit : MonoBehaviour {

	// Use this for initialization
	void Start () {
		foreach (var enemy in GetComponentsInChildren<CyclopsEnemy>())
		{
			enemy.GameStarted();
		}
		
	}
}
