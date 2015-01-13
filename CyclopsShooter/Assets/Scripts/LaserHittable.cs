using UnityEngine;
using System.Collections;

public abstract class LaserHittable : MonoBehaviour
{
	public float MaxHealth;
	public float Health { get; private set; }

	public bool IsDead { get { return Health <= 0; } }

	public void DoDamage(float amount)
	{
		if (IsDead) return;
		Health -= amount;
		if (IsDead)
		{
			OnDeath();
		}
	}

	void Awake()
	{
		Health = MaxHealth;
	}

	public abstract void OnDeath();
}
