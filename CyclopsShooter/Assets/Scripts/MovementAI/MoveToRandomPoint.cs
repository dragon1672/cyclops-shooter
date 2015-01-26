using UnityEngine;

public class MoveToRandomPoint : AINode
{
    public AINode[] To;
	public float MovePercentBoost = 1;
	public float AnglePercentBoost = 1;

    public override void EnterAction(CyclopsEnemy character, AINode previousMovementPoint)
    {
	    AINode toMoveTo = To[Random.Range(0, To.Length)];
		StartCoroutine(MoveToNewPointSpeed(character, toMoveTo, character.MovementSpeed * MovePercentBoost, character.AngleSpeed * AnglePercentBoost));
    }
}
