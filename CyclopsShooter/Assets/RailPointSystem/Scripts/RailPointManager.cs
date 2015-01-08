using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class RailPointManager : MonoBehaviour
{
    public GameObject HolderOfMoveToScript;
    public GameObject HolderOfLookAtScript;
    public string TagNameToGrabRailPoints = "RailPoint";
    public float Delay = 0.0f;
    public bool LoopPath = false;

    public bool AtEndRailPoint { get { return (_currentIndex >= _allRailPoints.Length); } }
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
    }

	public void Init()
	{
		_currentIndex = 0;
		_point = _allRailPoints[_currentIndex];
		HolderOfMoveToScript.gameObject.transform.position = _point.gameObject.transform.position;
		_currentIndex++;
		GameObject nextLookAt = GetNextLookAt();

		_point = _allRailPoints[_currentIndex];
		HolderOfLookAtScript.GetComponent<LookAtTarget>().TargetToLookAt = nextLookAt;
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
	    HolderOfLookAtScript.GetComponent<LookAtTarget>().CanUpdate = _moving;
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
            }
            else
            {
                _moving = false;
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
       HolderOfLookAtScript.GetComponent<LookAtTarget>().TargetToLookAt = nextLookAt;
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

    int GetIndexFromRailPointName(string src)
    {
        int ret = 0;
        int power = 1;
        for (int i = src.Length - 1; i >= 0; i--)
        {
            if ('0' <= src[i] && src[i] <= '9')
            {
                ret += ((int)src[i] - (int)'0') * power;
                power *= 10;
            }
        }
        return ret;
    }
}