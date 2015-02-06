using UnityEngine;
using System.Collections;

public class StandAndShoot : AINode {

	public float MinTimeBetweenShoot = 1f;
	public float MaxTimeBetweenShoot = 2f;

	public override void EnterAction(CyclopsEnemy character, AINode previousMovementPoint)
	{
        //Animation for shooting
        if (!character.AC.IsShooting)
        {
            character.AC.setAllAnimationsFalse();
            character.AC.IsShooting = true;
        }
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
