using UnityEngine;
using System.Collections;



public class CyclopsMainPlayer : MonoBehaviour
{
	public float MaxHealth = 3;
	public float HealthRegenPercentPerSecond = 0;
	public float HealthRegenDelay = 1;
	private float _currentHealthRegenDelay;
	public float HealthPoints { get; private set; }
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

	// Use this for initialization
	void Start ()
	{
		HealthPercent = 1;
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

	void EnemyFiredShot()
	{
		if (Random.Range(0, 100) < 30)
		{
			DoDamage(1);
		}
	}


	void DoDamage(float points)
	{
		if (IsDead) return;

		HealthPoints -= points;
		_currentHealthRegenDelay = HealthRegenDelay;
		if (!IsDead) return;

		if (OnDeathEvent != null) OnDeathEvent();
	}
}
