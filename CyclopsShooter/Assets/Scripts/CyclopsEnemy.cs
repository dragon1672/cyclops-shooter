using System.Linq;
using TBE_3DCore;
using UnityEngine;
using System.Collections;

public class CyclopsEnemy : LaserHittable
{
	public GameObject VisualGameObject = null;
	public AudioClip ShootAudioClip;
	public GameObject DestroyEffectObject;
    public AINode CurrentAIPoint;

	public float MovementSpeed = 2;
	public float AngleSpeed = 30;

	/// <summary>
	/// how long to wait after GameStart has been called
	/// </summary>
	public float DelayToStart = 0;

	private bool _activeInGame = false;

    void Start()
    {
        GameStarted(); // ANTHONY DEBUGGING
    }

	public void GameStarted()
	{
		StartCoroutine(StartMovement(DelayToStart));
	}

	private IEnumerator StartMovement(float waitTime)
	{
		yield return new WaitForSeconds(waitTime);
		_activeInGame = true;
		gameObject.SetActive(true);
        CurrentAIPoint.EnterAction(this,null); // init AI
	}

	// Update is called once per frame
	private void Update()
	{
		if (!_activeInGame) return;
	}

	public void Shoot()
	{
		Debug.Log("Shooting");
		//GetComponent<AudioManager>().PlayClip(ShootAudioClip);
		//random chance to hit player
		//BroadcastMessage("EnemyFiredShot");
	}

	public override void OnDeath()
	{
		TBE_Source tbe_3DSound = GetComponent<TBE_Source>();
		if (tbe_3DSound != null)
		{
			Destroy(tbe_3DSound, tbe_3DSound.clip.length);
		}
		VisualGameObject.SetActive(false);
		DestroyEffectObject.SetActive(true);
		Instantiate(DestroyEffectObject, transform.position, transform.rotation);
	}
}