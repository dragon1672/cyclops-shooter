using UnityEngine;
using System.Collections;

public class MovementExaggerate : MonoBehaviour
{
	public Vector3 Maginification;
	public Transform toBaseOffOf;
	
	void Update()
	{
		transform.localPosition = toBaseOffOf.localPosition;
		transform.localPosition.Scale(new Vector3(Maginification.x-1,Maginification.y-1,Maginification.z-1));
	}
}
