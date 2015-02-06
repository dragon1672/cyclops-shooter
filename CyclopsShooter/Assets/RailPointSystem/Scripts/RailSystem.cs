using UnityEngine;

public delegate void VoidAction();

public class RailSystem : MonoBehaviour
{
	//Order matters
	public RailPointManager[] Rails = new RailPointManager[3];

	public bool Smooth = true;
	public bool SnapToFirst = true;

    public VoidAction CompletedSection = null;
    public VoidAction EnterSection = null;
    public VoidAction CompletedAll = null;

    public bool InMovement { get; private set; }
    public bool InLevel { get; private set; }
    public bool Complete { get; private set; }
	
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

        InMovement = false;
        InLevel = true;
        if (EnterSection != null) EnterSection();
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

	public void Continue()
	{
		if (_currentIsDone)
		{
			UnRegisterCurrent();
			_currentManagerIndex++;
            if (_currentManagerIndex < Rails.Length)
            {
                InMovement = true;
                InLevel = false;
                if (CompletedSection != null) CompletedSection();
                RegisterCurrent();
            }
            else
            {
                Complete = true;
                if (CompletedAll != null) CompletedAll();
            }
		}
	}
}