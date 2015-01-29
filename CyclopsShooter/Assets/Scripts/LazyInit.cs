using System;
using UnityEngine;
using System.Collections;

class LazyInit<T>
{
	private readonly Func<T> _initFunction;
	private bool _created = false;
	private T _back;

	public T Get
	{
		get
		{
			if (!_created) _back = _initFunction();
			_created = true;
			return _back;
		}
	}

	public LazyInit(Func<T> initFunction)
	{
		_initFunction = initFunction;
	}
}
