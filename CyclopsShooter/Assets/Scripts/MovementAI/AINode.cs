using UnityEngine;
using System;
using System.Collections;


public abstract class AINode : MonoBehaviour {

#if UNITY_EDITOR
	private void Reset()
	{
		AINode oldItem = GetComponent<AINode>();
		if (oldItem == null || oldItem == this) return;

		if (UnityEditor.EditorUtility.DisplayDialog("Component already exists", "Do you want to replace it?",
			"Ok, replace it", "No, thanks!"))
		{
			gameObject.AddComponent<DeleteComponent>().componentReference = oldItem;
		}
		else
		{
			gameObject.AddComponent<DeleteComponent>().componentReference = this;
		}
	}
#endif


	public virtual void Awake () { }
	public virtual void Start() { }
	
	public virtual void Update () { }

	/// <summary>
	/// Called when Character completed MoveToNewPoint
	/// </summary>
	/// <param name="character">character acting on</param>
	/// <param name="previousMovementPoint">movement script that everything came from</param>
	public abstract void EnterAction(CyclopsEnemy character, AINode previousMovementPoint);

	/// <summary>
	/// Called when character first calls MoveToNewPoint
	/// </summary>
	/// <param name="character">character acting on</param>
	public virtual void ExitAction(CyclopsEnemy character) { }

	protected IEnumerator MoveToNewPointSpeed(CyclopsEnemy character, AINode pt, float movementSpeed, float angleSpeed, float delay = 0)
    {
		yield return new WaitForSeconds(delay);
		StartCoroutine(Movement(character, pt, movementDist => movementSpeed / movementDist, angleDist => angleSpeed / angleDist));
	}
    protected IEnumerator MoveToNewPointPercent(CyclopsEnemy character, AINode pt, float movementTime, float angleTime, float delay = 0)
    {
		yield return new WaitForSeconds(delay);
        StartCoroutine(Movement(character, pt, movementDist => 1 / movementTime, angleDist => 1 / angleTime));
    }
    private IEnumerator Movement(CyclopsEnemy character, AINode pt, Func<float,float> distPercentFun, Func<float, float> anglePercentFun)
    {
        ExitAction(character);
        float distance, angleDist;
        while ((distance = (pt.transform.position - character.transform.position).magnitude) > .001 | (angleDist = Quaternion.Angle(pt.transform.rotation, character.transform.rotation)) > .001)
        {
	        float distPercent = Mathf.Clamp(distPercentFun(distance) * Time.deltaTime, 0, 1);
	        float anglePercent = Mathf.Clamp(anglePercentFun(angleDist) * Time.deltaTime, 0, 1);

            character.transform.position = Vector3.Lerp(character.transform.position, pt.transform.position, distPercent);
            character.transform.rotation = Quaternion.Slerp(character.transform.rotation, pt.transform.rotation, anglePercent);
            yield return null;
        }
        character.transform.position = pt.transform.position;
        character.transform.rotation = pt.transform.rotation;
        character.CurrentAIPoint = pt;
        pt.EnterAction(character,this);
    }
}
