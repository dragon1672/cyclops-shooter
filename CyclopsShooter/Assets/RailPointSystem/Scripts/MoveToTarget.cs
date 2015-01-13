using UnityEngine;
using System.Collections;

public class MoveToTarget : MonoBehaviour
{
    public GameObject targetToMoveTo;
    public float speed = 5.0f;
    public bool CanUpdate { get; set; }
	public bool Complete { get; private set; }

	void Awake()
	{
		Complete = false;
        CanUpdate = true;
    }
    void Update()
    {
		if (CanUpdate && !Complete)
        {
            Move(targetToMoveTo.transform.position);
        }
    }

    public void Move(Vector3 posToMoveTo)
    {
        transform.position = Vector3.MoveTowards(transform.position, posToMoveTo, Time.deltaTime * speed);
	    Complete = (transform.position == posToMoveTo);
    }
}