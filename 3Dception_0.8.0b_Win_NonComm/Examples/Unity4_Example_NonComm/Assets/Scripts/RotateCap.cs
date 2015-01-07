using UnityEngine;
using System.Collections;

public class RotateCap : MonoBehaviour {

	public float speed;
	public int direction;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
				if (direction > 0)
						transform.Rotate (Vector3.up * Time.deltaTime * speed);
				else
						transform.Rotate (Vector3.back * Time.deltaTime * speed);
		}
}
