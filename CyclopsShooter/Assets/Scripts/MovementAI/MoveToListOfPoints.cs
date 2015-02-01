using UnityEngine;
using System.Collections;

public class MoveToListOfPoints : AINode
{
	public AINode[] Nodes;
	private int _currentIndex = 0;
	public float MovePercentBoost = 1;
	public float AnglePercentBoost = 1;


	public override void EnterAction(CyclopsEnemy character, AINode previousMovementPoint)
	{
        //Animation for walking
        if (!character.AC.IsWalking)
        {
            character.AC.setAllAnimationsFalse();
            character.AC.IsWalking = true;
        }
		AINode toMoveTo = Nodes[_currentIndex++];
		_currentIndex %= Nodes.Length;
		StartCoroutine(MoveToNewPointSpeed(character, toMoveTo, character.MovementSpeed * MovePercentBoost, character.AngleSpeed * AnglePercentBoost));
	}

	public override void EditorUpdate()
    {
		foreach (var node in Nodes)
		{
			Debug.DrawLine(transform.position, node.transform.position, Color.yellow);
		}
	}
}
