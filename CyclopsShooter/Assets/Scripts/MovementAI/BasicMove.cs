using UnityEngine;
using System.Collections;
using System;

public class BasicMove : AINode
{
    public AINode to;
	public float MovePercentBoost = 1;
	public float AnglePercentBoost = 1;
	public float Delay = 0;

    public override void EnterAction(CyclopsEnemy character, AINode previousMovementPoint)
    {
        StartCoroutine(MoveToNewPointSpeed(character, to, character.MovementSpeed * MovePercentBoost, character.AngleSpeed * AnglePercentBoost,Delay));
    }
}
