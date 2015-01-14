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

	private List<KeyCode> _acceptedKeyCodes;

    public float Speed{ get;set;}
	// Use this for initialization
	void Start () {
        particleSystem.enableEmission = false;
        destroyEffectObject.SetActive(false);
        Speed = 40f;
        //Turn Mouse Off
        Screen.lockCursor = true;

		_acceptedKeyCodes =
			Enum.GetValues(typeof (KeyCode)).Cast<KeyCode>().Except(new HashSet<KeyCode>() {
				KeyCode.Escape,
				KeyCode.LeftWindows,
				KeyCode.KeypadEnter,
				KeyCode.LeftShift,
				KeyCode.RightShift,
			}).ToList();
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

	Color Hex2Col(int num)
	{
		const int hexLength = 6;
		int maxHex = (int)Math.Pow(2,hexLength*4);
		num %= maxHex; // max hex val
		string hexValue = num.ToString("X");
		hexValue += new string('0', hexLength - hexValue.Length);
		return new Color(
			int.Parse(hexValue.Substring(0,2), System.Globalization.NumberStyles.HexNumber) / 255.0f,
			int.Parse(hexValue.Substring(2,2), System.Globalization.NumberStyles.HexNumber) / 255.0f,
			int.Parse(hexValue.Substring(4,2), System.Globalization.NumberStyles.HexNumber) / 255.0f
			);
	}

    IEnumerator FireLaser()
    {
        particleSystem.enableEmission = true;
		while (_acceptedKeyCodes.Any(Input.GetKey))
		{
			Color color = Hex2Col(_acceptedKeyCodes.Where(Input.GetKey).Sum(n => (int) n));
           //keep particle system going
            particleSystem.startSpeed = Speed;
			particleSystem.startColor = color;
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