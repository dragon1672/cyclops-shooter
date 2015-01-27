using UnityEngine;
using System.Collections;

public class StandAndShoot : AINode {

	public float MinTimeBetweenShoot = 1f;
	public float MaxTimeBetweenShoot = 2f;

	public override void EnterAction(CyclopsEnemy character, AINode previousMovementPoint)
	{
		StartCoroutine(ShootLoop(character));
	}

	private IEnumerator ShootLoop(CyclopsEnemy character)
	{
		while (!character.IsDead)
		{
			float timeTillShoot = Random.Range(MinTimeBetweenShoot, MaxTimeBetweenShoot);
			yield return new WaitForSeconds(timeTillShoot);
			character.Shoot();
		}
	}
}
