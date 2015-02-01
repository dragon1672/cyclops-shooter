using UnityEngine;

public class MoveToRandomPoint : AINode
{
    public AINode[] Nodes;
	public float MovePercentBoost = 1;
	public float AnglePercentBoost = 1;
	public float Delay = 0;

    public override void EnterAction(CyclopsEnemy character, AINode previousMovementPoint)
    {
        AINode toMoveTo = Nodes[Random.Range(0, Nodes.Length)];
        //Animation for walking
        if (!character.AC.IsWalking)
        {
            character.AC.setAllAnimationsFalse();
            character.AC.IsWalking = true;
        }
		StartCoroutine(MoveToNewPointSpeed(character, toMoveTo, character.MovementSpeed * MovePercentBoost, character.AngleSpeed * AnglePercentBoost, Delay));
    }

	public override void EditorUpdate() {
		foreach (var node in Nodes)
		{
			Debug.DrawLine(transform.position, node.transform.position, Color.gray);
		}
	}
}
