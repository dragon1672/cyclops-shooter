using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Collections;

public class LaserBeamScript : MonoBehaviour {

    public ParticleSystem particleSystem;
    public string ButtonToFireLaser = "Fire1";

    public GameObject destroyEffectObject;
    private float destroyParicleAfter = 1.0f;

	private List<KeyCode> _acceptedKeyCodes = new List<KeyCode>()
	{
		KeyCode.Space, // main attack
		KeyCode.Mouse1, KeyCode.Mouse2, KeyCode.Mouse3, KeyCode.Mouse4, KeyCode.Mouse5, KeyCode.Mouse6, // mouse
		KeyCode.C, KeyCode.V, KeyCode.B, KeyCode.N, KeyCode.M, // bottom row
	};

    public float Speed{ get;set;}
	// Use this for initialization
	void Start () {
        particleSystem.enableEmission = false;
        destroyEffectObject.SetActive(false);
        Speed = 40f;
        //Turn Mouse Off
        Screen.lockCursor = true;

		_acceptedKeyCodes = new List<KeyCode>()
		{
			KeyCode.Space,
			KeyCode.X,
			KeyCode.C,
			KeyCode.V,
			KeyCode.B,
			KeyCode.N,
			KeyCode.M,
			KeyCode.Mouse0,
			KeyCode.Mouse1,
			KeyCode.Mouse2,
			KeyCode.Mouse3,
			KeyCode.Mouse4,
			KeyCode.Mouse5,
			KeyCode.Mouse6,
		};
	}
	
	// Update is called once per frame
	void Update () {
		if (_acceptedKeyCodes.Any(Input.GetKey))
        {
            //fail safe stop
            StopCoroutine("FireLaser");
            StartCoroutine("FireLaser");
        }
	}

    IEnumerator FireLaser()
    {
        particleSystem.enableEmission = true;
		while (_acceptedKeyCodes.Any(Input.GetKey))
		{
           //keep particle system going
            particleSystem.startSpeed = Speed;
            RaycastHit hitter;
            Physics.Raycast(transform.parent.transform.position, transform.parent.transform.forward, out hitter, 10000);
            if (hitter.collider != null)
            {
	            LaserHittable hittable = hitter.collider.GetComponent<LaserHittable>();
	            if (hittable != null)
	            {
		            hittable.DoDamage(1*Time.deltaTime);
	            }
			}
			 
     yield return null;
        }
        particleSystem.enableEmission = false;
    }
}