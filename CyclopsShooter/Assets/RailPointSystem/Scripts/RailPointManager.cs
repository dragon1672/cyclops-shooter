using System;
using System.Globalization;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public delegate void RailPointEvent();

public class RailPointManager : MonoBehaviour
{
	public GameObject HolderOfMoveToScript;
	public GameObject HolderOfLookAtScript;
	public string TagNameToGrabRailPoints = "RailPoint";
	public float Delay = 0.0f;
	public bool LoopPath = false;
	public bool SnapToStart = true;
    public bool UseLookAt = true;

	public RailPointEvent LoopEvent = null;
	public RailPointEvent CompletedEvent = null;

	public bool AtEndRailPoint
	{
		get { return (_currentIndex >= _allRailPoints.Length); }
	}

	private int _currentIndex;
	private GameObject _point;
	private GameObject[] _allRailPoints;
	public bool _moving { get; private set; }
	private bool isInitialized = false;

	// Use this for initialization
	void Start()
	{
		_allRailPoints = GameObject.FindGameObjectsWithTag(TagNameToGrabRailPoints);
		_allRailPoints = _allRailPoints.OrderBy(n => GetIndexFromRailPointName(n.name)).ToArray();

		if(_allRailPoints.Any(n=>n.GetComponent<RailPoint>()==null))
			Debug.LogException(new UnityException("Rail Points have tag, but not Rail Point Script"));

		if (enabled) Init();
	}

	public void Init()
	{
		_currentIndex = 0;
		_point = _allRailPoints[_currentIndex];
		if (SnapToStart)
		{
			HolderOfMoveToScript.gameObject.transform.position = _point.gameObject.transform.position;
		}
		_currentIndex++;
		GameObject nextLookAt = GetNextLookAt();

		_point = _allRailPoints[_currentIndex];
        if (UseLookAt)
        {
            HolderOfLookAtScript.GetComponent<LookAtTarget>().TargetToLookAt = nextLookAt;
        }
		HolderOfMoveToScript.GetComponent<MoveToTarget>().targetToMoveTo = _point;
		isInitialized = true;
		StartCoroutine(WaitTillGo(Delay));
	}

	// Update is called once per frame
	void Update()
	{
		if (!enabled) return;
		if (!isInitialized)
		{
			Init();
		}
		if (!_moving) return;
		UpdateCurrentIndex();
		UpdateMoveAbility();
        if (UseLookAt)
        {
            HolderOfLookAtScript.GetComponent<LookAtTarget>().CanUpdate = _moving;
        }
		HolderOfMoveToScript.GetComponent<MoveToTarget>().CanUpdate = _moving;
	}

	private void UpdateCurrentIndex()
	{
		_point = _allRailPoints[_currentIndex];
		RailPoint rp = _point.GetComponent<RailPoint>();

		if (rp.HasBeenReached)
		{
			// Debug.Log("Index reached: " + currentIndex);
			_currentIndex++;
		}
	}

	private void UpdateMoveAbility()
	{
		if (AtEndRailPoint)
		{
			if (LoopPath)
			{
				_currentIndex = 0;
				if (LoopEvent != null)
				{
					LoopEvent();
				}
			}
			else
			{
				_moving = false;
				if (CompletedEvent != null)
				{
					CompletedEvent();
				}
			}
		}
		else
		{
			SetUpNextPathTo();
		}
	}

	private void SetUpNextPathTo()
	{
		GameObject nextLookAt = GetNextLookAt();
        if (UseLookAt)
        {
            HolderOfLookAtScript.GetComponent<LookAtTarget>().TargetToLookAt = nextLookAt;
        }
		HolderOfMoveToScript.GetComponent<MoveToTarget>().targetToMoveTo = _point;
	}

	private GameObject GetNextLookAt()
	{
		_point = _allRailPoints[_currentIndex];
		RailPoint rp = _point.GetComponent<RailPoint>();
		GameObject nextLookAt = rp.ToLookAtWhenInRouteToMe;
		if (nextLookAt == null)
		{
			int pointIndex = _currentIndex;
			nextLookAt = _allRailPoints[pointIndex];
		}
		return nextLookAt;
	}

	private IEnumerator WaitTillGo(float time)
	{
		yield return new WaitForSeconds(time);
		_moving = true;
	}

	private int GetIndexFromRailPointName(string src)
	{
		int ret = 0;
		int power = 1;
		for (int i = src.Length - 1; i >= 0; i--)
		{
			if ('0' <= src[i] && src[i] <= '9')
			{
				ret += ((int) src[i] - (int) '0')*power;
				power *= 10;
			}
		}
		return ret;
	}
}