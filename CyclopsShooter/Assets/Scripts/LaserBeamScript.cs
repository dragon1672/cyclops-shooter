using UnityEngine;
using System.Collections;

public class LaserBeamScript : MonoBehaviour {

    public ParticleSystem particleSystem;
    public string ButtonToFireLaser = "Fire1";

    public float Speed{ get;set;}
	// Use this for initialization
	void Start () {
        particleSystem.enableEmission = false;
        Speed = 40f;
        //Turn Mouse Off
        Screen.lockCursor = true;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown(ButtonToFireLaser))
        {
            //fail safe stop
            StopCoroutine("FireLaser");
            StartCoroutine("FireLaser");
        }
	}

    IEnumerator FireLaser()
    {
        particleSystem.enableEmission = true;
        while (Input.GetButton(ButtonToFireLaser))
        {
           //keep particle system going
            particleSystem.startSpeed = Speed;
            yield return null;
        }
        particleSystem.enableEmission = false;
    }
}