using UnityEngine;
using System.Collections;

public class LaserBeamScript : MonoBehaviour {

    public ParticleSystem particleSystem;
    public string ButtonToFireLaser = "Fire1";

    public GameObject destroyEffectObject;
    private float destroyParicleAfter = 1.0f;

    public float Speed{ get;set;}
	// Use this for initialization
	void Start () {
        particleSystem.enableEmission = false;
        destroyEffectObject.SetActive(false);
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
            RaycastHit hitter;
            Physics.Raycast(transform.parent.transform.position, transform.parent.transform.forward, out hitter, 10000);
            if (hitter.collider != null && hitter.collider.GetComponent<CyclopsEnemy>() != null)
            {
                destroyEffectObject.SetActive(true);
                GameObject enemy = hitter.collider.gameObject;
                Destroy(Instantiate(destroyEffectObject, enemy.transform.position, enemy.transform.rotation), destroyParicleAfter);
                enemy.SetActive(false);
            }
            yield return null;
        }
        particleSystem.enableEmission = false;
    }
}