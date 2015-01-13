using UnityEngine;
using System.Collections;

public class CyclopsEnemy : LaserHittable
{
	public GameObject VisualGameObject = null;
	public float MinTimeBetweenShots;
	public float MaxTimeBetweenShots;
	private bool _activeInGame = false;

	private float _timeSinceLastShot = 0;
	private float _nextShotTime = -1;

	void Awake()
	{
		gameObject.SetActive(false);
	}

	public void GameStarted()
	{
		_activeInGame = true;
		gameObject.SetActive(true);
	}

	// Update is called once per frame
	private void Update()
	{
		if (!_activeInGame) return;

		if (_timeSinceLastShot > MinTimeBetweenShots)
		{
			if (_timeSinceLastShot >= _nextShotTime)
			{
				Shoot();
				_timeSinceLastShot = 0;
			}
		}
		else
		{
			_nextShotTime = Random.Range(MinTimeBetweenShots, MaxTimeBetweenShots);
		}


		_timeSinceLastShot += Time.deltaTime;
	}

	private void Shoot()
	{
		//random chance to hit player
		BroadcastMessage("EnemyFiredShot");
	}

	public override void OnDeath()
	{

	}
}
