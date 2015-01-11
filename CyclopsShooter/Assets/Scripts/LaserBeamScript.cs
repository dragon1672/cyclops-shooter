using UnityEngine;
using System.Collections;

public class LaserBeamScript : MonoBehaviour {

    public LineRenderer line;
    public ParticleSystem particleSystem;
    public GameObject aimSpot;
	// Use this for initialization
	void Start () {
        line.enabled = false;
        particleSystem.enableEmission = false;
        //Turn Mouse Off
        Screen.lockCursor = true;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Fire1"))
        {
            //fail safe stop
            StopCoroutine("FireLaser");
            StartCoroutine("FireLaser");
        }
	}

    IEnumerator FireLaser()
    {
        line.enabled = true;
        particleSystem.enableEmission = true;
        while (Input.GetButton("Fire1"))
        {
            line.SetPosition(0, transform.position);
            line.SetPosition(1, aimSpot.transform.position);

            yield return null;
        }
        line.enabled = false;
        particleSystem.enableEmission = false;
    }
}
