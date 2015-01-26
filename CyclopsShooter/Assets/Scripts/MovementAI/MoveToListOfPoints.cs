using UnityEngine;
using System.Collections;

public class MoveToListOfPoints : AINode
{
	public AINode[] nodes;
	private int CurrentIndex = 0;
	public float MovePercentBoost = 1;
	public float AnglePercentBoost = 1;


	public override void EnterAction(CyclopsEnemy character, AINode previousMovementPoint)
	{
		AINode toMoveTo = nodes[CurrentIndex++];
		CurrentIndex %= nodes.Length;
		StartCoroutine(MoveToNewPointSpeed(character, toMoveTo, character.MovementSpeed * MovePercentBoost, character.AngleSpeed * AnglePercentBoost));
	}
}
