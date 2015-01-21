using UnityEngine;
using System.Collections;

public class CrouchMe : MonoBehaviour {
    public float ChangeHeightAmount = 1.0f;
    public float Speed = 4.0f;
    
	private bool _isCrouching;
	private float _amountLeftToChange = 0;


	// Update is called once per frame
	void Update () {
        if(Input.GetButtonDown("Fire2"))
        {
	        CrouchDown();
        }
        else if (Input.GetButtonUp("Fire2"))
        {
	        UnCrouch();
        }
	}
	public void CrouchDown()
	{
		if (_isCrouching) return;
		_amountLeftToChange -= Mathf.Abs(ChangeHeightAmount);
		StopAllCoroutines();
		StartCoroutine(CrouchDownRoutine());
	}

	public void UnCrouch()
	{
		if (!_isCrouching) return;
		_amountLeftToChange += Mathf.Abs(ChangeHeightAmount);
		StopAllCoroutines();
		StartCoroutine(UnCrouchRoutine());
	}
	private IEnumerator CrouchDownRoutine()
	{
		Debug.Log("Down Enter");
		_isCrouching = true;
		do
		{
			Debug.Log("Down " + _amountLeftToChange);
			gameObject.transform.position -= new Vector3(0, Speed*Time.deltaTime, 0);
			_amountLeftToChange += Speed*Time.deltaTime;
			_amountLeftToChange = Mathf.Min(0, _amountLeftToChange);
			yield return null;
		} while (_amountLeftToChange < 0);
	}

	private IEnumerator UnCrouchRoutine()
	{
		Debug.Log("Up Enter");
		_isCrouching = false;
		do
		{
			Debug.Log("Up " + _amountLeftToChange);
			gameObject.transform.position += new Vector3(0, Speed*Time.deltaTime, 0);
			_amountLeftToChange -= Speed*Time.deltaTime;
			_amountLeftToChange = Mathf.Max(0, _amountLeftToChange);
			yield return null;
		} while (_amountLeftToChange > 0);
	}
}
