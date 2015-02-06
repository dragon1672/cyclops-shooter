using System.Linq;
using UnityEngine;
using System.Collections;

public class ShootAndReturn : AINode
{
	public float MinTimeToPeakHeadOut = .75f;
	public float MaxTimeToPeakHeadOut = 1.5f;

	public float MinTimeBetweenShoot = .1f;
	public float MaxTimeBetweenShoot = .5f;

	public float MoveSpeedBoost = 1;
	public float AngleSpeedBoost = 1;

	public override void EnterAction(CyclopsEnemy character, AINode previousMovementPoint)
	{
        //Animation for shooting
        if (!character.AC.IsShooting)
        {
            character.AC.setAllAnimationsFalse();
            character.AC.IsShooting = true;
        }
		StartCoroutine(DoShootAndReturn(character, previousMovementPoint));
	}

	private IEnumerator DoShootAndReturn(CyclopsEnemy character, AINode previousMovementPoint)
	{
		float timeTillReturn = Random.Range(MinTimeToPeakHeadOut, MaxTimeToPeakHeadOut);
        float timeTillShoot;

		while ((timeTillShoot = Random.Range(MinTimeBetweenShoot, MaxTimeBetweenShoot)) < timeTillReturn)
		{
			yield return new WaitForSeconds(timeTillShoot);
			timeTillReturn -= timeTillShoot;
			character.Shoot();
		}
        yield return new WaitForSeconds(timeTillReturn);
		StartCoroutine(MoveToNewPointSpeed(character, previousMovementPoint, character.MovementSpeed * MoveSpeedBoost, character.AngleSpeed * AngleSpeedBoost));
	}
}
