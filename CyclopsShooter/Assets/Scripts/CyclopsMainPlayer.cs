﻿using UnityEngine;
using System.Collections;
using System.ComponentModel;


[RequireComponent(typeof(CrouchMe))]
public class CyclopsMainPlayer : MonoBehaviour
{
	public float MaxHealth = 3;
	public float HealthRegenPercentPerSecond = 0;
	public float HealthRegenDelay = 1;
	private float _currentHealthRegenDelay;

	[Range(0,100)] public float DamageChance = 30;
	public float DamageAmount= .5f;
	[SerializeField]
	private float _healthPoints;

	public VisualHealth VisualEffect;
	public float HealthPoints {
		get { return _healthPoints; }
		private set {
			_healthPoints = value;
			VisualEffect.Alpha = 1-HealthPercent;
		}
	}
	public float HealthPercent
	{
		get { return HealthPoints / MaxHealth; }
		private set { HealthPoints = MaxHealth * value; }
	}

	private bool IsDead
	{
		get { return HealthPoints <= 0; }
	}

	public VoidAction OnDeathEvent = null;

	private CrouchMe crouchScript;

	void Start ()
	{
		crouchScript = GetComponent<CrouchMe>();
		HealthPercent = 1;
		DoDamage(.01f);
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (IsDead) return;

		if (_currentHealthRegenDelay <= 0 && HealthPercent < 1)
		{
			HealthPercent = Mathf.Min(1, HealthPercent + HealthRegenPercentPerSecond*Time.deltaTime);
		}
		_currentHealthRegenDelay -= Time.deltaTime;
	}

	public void EnemyFiredShot()
	{
		if (Random.Range(0, 100) < DamageChance)
		{
			if (!crouchScript || Random.Range(0, 1) <= .5)
			{
				//Debug.Log("Hit");
				DoDamage(DamageAmount);
			}
		}
	}

	void DoDamage(float points)
	{
		if (IsDead) return;

		HealthPoints -= points;
		_currentHealthRegenDelay = HealthRegenDelay;
		if (!IsDead) return;

		if (OnDeathEvent != null) OnDeathEvent();
		Debug.Log("Dead");
        StartCoroutine(GameOver(3.0f));
	}

    IEnumerator GameOver(float secToWait)
    {
        LaserBeamScript[] lasers = GetComponentsInChildren<LaserBeamScript>();
        foreach (LaserBeamScript l in lasers)
        {
            l.gameObject.SetActive(false);
        }
        return CyclopGameManager.GameOver(secToWait);
    }
}