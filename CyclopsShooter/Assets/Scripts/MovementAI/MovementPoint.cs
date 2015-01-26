using UnityEngine;
using System;
using System.Collections;

public abstract class MovementPoint : MonoBehaviour {

	public virtual void Awake () { }
	public virtual void Start() { }
	
	public virtual void Update () { }

    /// <summary>
    /// Called when Character completed MoveToNewPoint
    /// </summary>
    /// <param name="character">character acting on</param>
    public abstract void EnterAction(CyclopsEnemy character);
    /// <summary>
    /// Called when character first calls MoveToNewPoint
    /// </summary>
    /// <param name="character">character acting on</param>
    public abstract void ExitAction(CyclopsEnemy character);

    protected IEnumerator MoveToNewPointSpeed(CyclopsEnemy character, MovementPoint pt, float movementSpeed, float angleSpeed)
    {
        return Movement(character, pt, movementDist => movementSpeed / movementDist, angleDist => angleSpeed / angleDist);
    }
    protected IEnumerator MoveToNewPointPercent(CyclopsEnemy character, MovementPoint pt, float movementTime, float angleTime)
    {
        return Movement(character, pt, movementDist => 1 / movementTime, angleDist => 1 / angleTime);
    }
    private IEnumerator Movement(CyclopsEnemy character, MovementPoint pt, Func<float,float> distPercentFun, Func<float, float> anglePercentFun)
    {
        ExitAction(character);
        float distance, angleDist;
        while ((distance = (pt.transform.position - character.transform.position).magnitude) > .001 && (angleDist = Quaternion.Angle(pt.transform.rotation, character.transform.rotation)) > .001)
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
        pt.EnterAction(character);
    }
}
