using System.Linq;
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SoldierAnimationControls))]
[RequireComponent(typeof(CyclopsMainPlayer))]
public class CyclopsEnemy : LaserHittable
{
	public GameObject VisualGameObject = null;
	public AudioClip ShootAudioClip;
	public GameObject DestroyEffectObject;
    public AINode CurrentAIPoint;
    public SoldierAnimationControls AC { get; private set; }

	public AudioClip[] InitSoundsClips;

	private CyclopsMainPlayer _mainPlayer;
	private AudioManager _audioManager;

	public float MovementSpeed = 2;
	public float AngleSpeed = 30;

	/// <summary>
	/// how long to wait after GameStart has been called
	/// </summary>
	public float DelayToStart = 0;

	private bool _activeInGame = false;

	public void GameStarted()
	{
		StartCoroutine(StartMovement(DelayToStart));
		_mainPlayer = FindObjectOfType<CyclopsMainPlayer>();
		_audioManager = GetComponent<AudioManager>() ?? gameObject.AddComponent<AudioManager>();
        AC = GetComponent<SoldierAnimationControls>(); 
	}

	private IEnumerator StartMovement(float waitTime)
	{
		yield return new WaitForSeconds(waitTime);
		_activeInGame = true;
		gameObject.SetActive(true);
		if (InitSoundsClips.Length > 0)
		{
			_audioManager.PlayClip(InitSoundsClips[Random.Range(0,InitSoundsClips.Length)]);
		}
        CurrentAIPoint.EnterAction(this,null); // init AI
	}

	// Update is called once per frame
	private void Update()
	{
		if (!_activeInGame) return;
	}

	public void Shoot()
	{
		if (!_activeInGame) return;
		//Debug.Log("Shooting");
		_audioManager.PlayClip(ShootAudioClip);
		if (_mainPlayer != null)
			_mainPlayer.EnemyFiredShot(); //random chance to hit player
	}

	public override void OnDeath()
	{
		VisualGameObject.SetActive(false);
		_activeInGame = false;
		DestroyEffectObject.SetActive(true);
		Instantiate(DestroyEffectObject, transform.position, transform.rotation);
	}
}