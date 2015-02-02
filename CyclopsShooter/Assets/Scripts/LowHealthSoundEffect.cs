using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CyclopsMainPlayer))]
public class LowHealthSoundEffect : MonoBehaviour
{
	public float FadeSpeed;
	public AudioClip AudClip;
	[Range(0, 1)] public float LowPercentTrigger;
	[Range(0, 1)] public float HighPercentTrigger;
	[Range(0, 1)] public float MinVol;
	[Range(0, 1)] public float MaxVol;

	private AudioSource mySrc;
	private CyclopsMainPlayer player;

	// Use this for initialization
	void Start ()
	{
		mySrc = gameObject.AddComponent<AudioSource>();
		mySrc.volume = 0;
		mySrc.clip = AudClip;
		mySrc.loop = true;
		mySrc.Play();
		player = GetComponent<CyclopsMainPlayer>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		float health = player.HealthPercent;
		float targetVolume;
		if (LowPercentTrigger <= health && health <= HighPercentTrigger)
		{
			float range = HighPercentTrigger - LowPercentTrigger;
			float targetVolumePercent = 1 - (health - LowPercentTrigger) / range;
			targetVolume = targetVolumePercent * (MaxVol - MinVol) + MinVol;
		}
		else
		{
			targetVolume = 0;
		}
		mySrc.volume = Mathf.Lerp(mySrc.volume, targetVolume, Time.deltaTime * FadeSpeed);
	}
}
