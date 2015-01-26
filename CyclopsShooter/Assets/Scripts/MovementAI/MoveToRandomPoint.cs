using UnityEngine;

public class MoveToRandomPoint : AINode
{
    public AINode[] To;
    public float MoveSpeedLow = 1;
    public float MoveSpeedHigh = 2;
    public float AngleSpeedLow = 180;
    public float AngleSpeedHigh = 360;
    public override void EnterAction(CyclopsEnemy character, AINode previousMovementPoint)
    {
	    AINode toMoveTo = To[Random.Range(0, To.Length)];
		StartCoroutine(MoveToNewPointSpeed(character, toMoveTo, Random.Range(MoveSpeedLow, MoveSpeedHigh), Random.Range(AngleSpeedLow, AngleSpeedHigh)));
    }

    public override void ExitAction(CyclopsEnemy character) { }
}
