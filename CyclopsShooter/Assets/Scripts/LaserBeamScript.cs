using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Collections;

public class LaserBeamScript : MonoBehaviour {

    public ParticleSystem LaserParticleSystem;
    public string ButtonToFireLaser = "Fire1";

    private float _laserInEffectCounter;
    private const float LaserCooldown = 0.7f;
    private const float TimeCooldown = 0.5f + LaserCooldown;
    private bool _firing;

	private AudioSource aud;

	private List<KeyCode> _acceptedKeyCodes = new List<KeyCode>()
	{
		KeyCode.Space, // main attack
		KeyCode.Mouse1, KeyCode.Mouse2, KeyCode.Mouse3, KeyCode.Mouse4, KeyCode.Mouse5, KeyCode.Mouse6, // mouse
		KeyCode.C, KeyCode.V, KeyCode.B, KeyCode.N, KeyCode.M, // bottom row
	};

    public float Speed{ get;set;}
	// Use this for initialization
	void Start () {
        LaserParticleSystem.enableEmission = false;
        Speed = 40f;
        //Turn Mouse Off
        Screen.lockCursor = true;
        _firing = false;

		aud = GetComponent<AudioSource>();

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
			//KeyCode.Mouse1,
			KeyCode.Mouse2,
			KeyCode.Mouse3,
			KeyCode.Mouse4,
			KeyCode.Mouse5,
			KeyCode.Mouse6,
		};
	}
	
	// Update is called once per frame
	void Update () {
        if (_acceptedKeyCodes.Any(Input.GetKeyDown) && !_firing)
        {
            if (_laserInEffectCounter > TimeCooldown)
            {
                _laserInEffectCounter = 0.0f;
                _firing = true;
                StartCoroutine("FireLaser");
            }
        }

        if (!_firing && _laserInEffectCounter < TimeCooldown)
        {
            _laserInEffectCounter += Time.deltaTime;
        }
	}

    IEnumerator FireLaser()
    {
        LaserParticleSystem.enableEmission = true;
        this.gameObject.audio.enabled = true;
        LaserParticleSystem.startSpeed = Speed;
		aud.Play();
		//continue firing laser
		while (_laserInEffectCounter < LaserCooldown)
        {
            _laserInEffectCounter += Time.deltaTime;
            RaycastHit hitter;
            Physics.Raycast(transform.parent.transform.position, transform.parent.transform.forward, out hitter, 10000);
	        if (hitter.collider != null)
	        {
		        var player = hitter.collider.GetComponent<LaserHittable>();
		        if (player != null)
		        {
			        player.DoDamage(1*Time.deltaTime);
		        }
	        }

	        yield return null;
        }
		aud.Stop();
        this.gameObject.audio.enabled = false;
        LaserParticleSystem.enableEmission = false;
        _firing = false;
    }
}