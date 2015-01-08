using UnityEngine;

public class RailSystem : MonoBehaviour
{
	//Order matters
	public RailPointManager[] Rails = new RailPointManager[3];

	public bool Smooth = true;
	public bool SnapToFirst = true;
	
	private int _currentManagerIndex = 0;
	private bool _currentIsDone = false;
	private bool _isInit = false;

	void Awake()
	{
		foreach (RailPointManager t in Rails)
		{
			t.enabled = false;
		}
	}

	private void Init()
	{
		RegisterCurrent();
		_isInit = true;
	}

	private void RegisterCurrent()
	{
		_currentIsDone = false;
		Rails[_currentManagerIndex].CompletedEvent += UnRegisterCurrent;
		Rails[_currentManagerIndex].enabled = true;
		Rails[_currentManagerIndex].LoopPath = false;
		Rails[_currentManagerIndex].SnapToStart = !Smooth || SnapToFirst && _currentManagerIndex == 0;
	}

	private void UnRegisterCurrent()
	{
		_currentIsDone = true;
		Rails[_currentManagerIndex].CompletedEvent -= UnRegisterCurrent;
		Rails[_currentManagerIndex].enabled = false;
	}

	// Update is called once per frame
	private void Update()
	{
		if (enabled && !_isInit)
		{
			Init();
		}
		//THIS IS DEBUG
		if (Input.GetKey(KeyCode.A))
		{
			Debug.Log("Hit A");
			Continue();
		}
	}

	private void Continue()
	{
		if (_currentIsDone)
		{
			UnRegisterCurrent();
			_currentManagerIndex++;
			if (_currentManagerIndex < Rails.Length)
			{
				RegisterCurrent();
			}
		}
	}
}
