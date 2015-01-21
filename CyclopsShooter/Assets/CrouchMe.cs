using UnityEngine;
using System.Collections;

public class CrouchMe : MonoBehaviour {
    public float changeHeightAmount = 1.0f;
    public float speed = 2.0f;
    private bool isCrouching;
    private float originalY;
    Vector3 offset;
    Vector3 original;
    void Start()
    {
        original = this.gameObject.transform.position;
        originalY = original.y;
    }
	// Update is called once per frame
	void Update () {
        if(Input.GetButtonDown("Fire2"))
        {
            original = new Vector3(this.gameObject.transform.position.x, originalY, this.gameObject.transform.position.z);
            offset = new Vector3(0, -changeHeightAmount, 0);
            isCrouching = true;
        }
        else if (Input.GetButtonUp("Fire2"))
        {
            offset = Vector3.zero;
            isCrouching = false;
        }
        if (isCrouching)
        {
            if (this.gameObject.transform.position != original + offset)
            {
                this.gameObject.transform.position = Vector3.Lerp(this.gameObject.transform.position, original + offset, speed*Time.deltaTime);
            }
        }
        else
        {
            if (this.gameObject.transform.position != original)
            {
                this.gameObject.transform.position = Vector3.Lerp(this.gameObject.transform.position, original, speed*Time.deltaTime);
            }
        }
	}
}
