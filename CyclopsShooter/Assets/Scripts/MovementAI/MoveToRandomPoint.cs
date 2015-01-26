using UnityEngine;

public class MoveToRandomPoint : AINode
{
    public AINode[] Nodes;
	public float MovePercentBoost = 1;
	public float AnglePercentBoost = 1;

    public override void EnterAction(CyclopsEnemy character, AINode previousMovementPoint)
    {
	    AINode toMoveTo = Nodes[Random.Range(0, Nodes.Length)];
		StartCoroutine(MoveToNewPointSpeed(character, toMoveTo, character.MovementSpeed * MovePercentBoost, character.AngleSpeed * AnglePercentBoost));
    }

	public override void EditorUpdate() {
		foreach (var node in Nodes)
		{
			Debug.DrawLine(transform.position, node.transform.position, Color.gray);
		}
	}
}
