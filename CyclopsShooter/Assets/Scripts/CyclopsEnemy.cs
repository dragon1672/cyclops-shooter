using System.Linq;
using TBE_3DCore;
using UnityEngine;
using System.Collections;

public class CyclopsEnemy : LaserHittable
{
	public GameObject VisualGameObject = null;
	public float MinTimeBetweenShots;
	public float MaxTimeBetweenShots;
	/// <summary>
	/// how long to wait after GameStart has been called
	/// </summary>
	public float DelayToStart = 0;

	private float _currentDelay;
	private bool _activeInGame = false;

	private float _timeSinceLastShot = 0;
	private float _nextShotTime = -1;

	public void GameStarted()
	{
		_activeInGame = true;
		_currentDelay = DelayToStart;
		_timeSinceLastShot = 0;
		_nextShotTime = Random.Range(MinTimeBetweenShots, MaxTimeBetweenShots);
		StartCoroutine(StartMovement(DelayToStart));
	}

	private IEnumerator StartMovement(float waitTime)
	{
		yield return new WaitForSeconds(waitTime);
		gameObject.SetActive(true);
		GetComponentInChildren<MoveToTarget>().enabled = true;
	}

	// Update is called once per frame
	private void Update()
	{
		if (!_activeInGame) return;

		if (_currentDelay >= 0)
		{
			_currentDelay -= Time.deltaTime;
			return;
		}

		if (_timeSinceLastShot > MinTimeBetweenShots)
		{
			if (_timeSinceLastShot >= _nextShotTime)
			{
				_timeSinceLastShot = 0;
				_nextShotTime = Random.Range(MinTimeBetweenShots, MaxTimeBetweenShots);
				Shoot();
			}
		}
		_timeSinceLastShot += Time.deltaTime;
	}

	private void Shoot()
	{
		TBE_Source tbe_3DSound = GetComponent<TBE_Source>();
		AudioSource unityAudio;
		if (tbe_3DSound != null)
		{
			tbe_3DSound.PlayOneShot(tbe_3DSound.clip);
			Debug.Log(gameObject.name+": Fire Played 3D");
		}
		else if ((unityAudio = GetComponent<AudioSource>()) != null)
		{
			unityAudio.PlayOneShot(unityAudio.clip);
			Debug.Log(gameObject.name+": Fire Played Unity");
		}
		//random chance to hit player
		//BroadcastMessage("EnemyFiredShot");
	}

	public override void OnDeath()
	{
		VisualGameObject.SetActive(false);
		//destroyEffectObject.SetActive(true);
		//Destroy(Instantiate(destroyEffectObject, enemy.transform.position, enemy.transform.rotation), destroyParicleAfter);
	}
}
