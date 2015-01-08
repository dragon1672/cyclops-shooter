using System;
using System.Threading;
using UnityEngine;
using System.Collections;

public class CyclopsPlayer : MonoBehaviour
{
	public static T Clamp<T>(T val, T min, T max) where T : IComparable<T>
	{
		if (val.CompareTo(min) < 0) return min;
		else if (val.CompareTo(max) > 0) return max;
		else return val;
	}
	class KeyManager
	{
		private readonly Func<bool> _checkFunction;
		private int _framesPressed;
		public KeyManager(Func<bool> checkFunction)
		{
			this._checkFunction = checkFunction;
		}

		internal void Update()
		{
			_framesPressed = _checkFunction() ? _framesPressed + 1 : 0;
		}

		internal bool WasPressed()
		{
			return _framesPressed == 1;
		}
	}

	public double HealthMax;
	private double _health;
	public double Health {
		get { return _health; }
		set { _health = Clamp(value,0,HealthMax); }
	}

	public float HealthPercent { get { return (float)(Health/HealthMax); } }

	private KeyManager FireButton;
	public double MaxCoolDown;
	private double _coolDown;
	private double CoolDown
	{
		get { return _coolDown; }
		set { _coolDown = Clamp(value,0,MaxCoolDown); }
	}

	void Start ()
	{
		FireButton = new KeyManager(() => Input.GetMouseButtonDown(0));
		Health = HealthMax;
	}

	void Update()
	{
		FireButton.Update();
		if (FireButton.WasPressed() || CoolDown==0)
		{
			CoolDown = MaxCoolDown;
		}

		CoolDown -= Time.deltaTime;
	}
}
