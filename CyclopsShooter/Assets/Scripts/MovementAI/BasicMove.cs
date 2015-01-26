using UnityEngine;
using System.Collections;
using System;

public class BasicMove : AINode
{
    public AINode to;
    public float MoveSpeed = 2;
    public float AngleSpeed = 180;

    public override void EnterAction(CyclopsEnemy character, AINode previousMovementPoint)
    {
        StartCoroutine(MoveToNewPointSpeed(character, to, MoveSpeed, AngleSpeed));
    }

    public override void ExitAction(CyclopsEnemy character) { }
}
