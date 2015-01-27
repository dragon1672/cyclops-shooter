using UnityEngine;
using System.Collections;



public class CyclopsMainPlayer : MonoBehaviour
{
	public float MaxHealth = 3;
	public float HealthRegenPercentPerSecond = 0;
	public float HealthRegenDelay = 1;
	private float _currentHealthRegenDelay;
	private float _healthPoints;

	public VisualHealth VisualEffect;
	public float HealthPoints {
		get { return _healthPoints; }
		private set {
			_healthPoints = value;
			VisualEffect.Alpha = 1 / _healthPoints;
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

	// Use this for initialization
	void Start ()
	{
		Debug.Log(gameObject.name);
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

	public void EnemyFiredShot()
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
