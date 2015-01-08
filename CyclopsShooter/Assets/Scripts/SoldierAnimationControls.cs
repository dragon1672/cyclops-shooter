using UnityEngine;
using System.Collections;

public class SoldierAnimationControls : MonoBehaviour {

    public float walkAmount = -0.02f;

    private Animator _animator;
    private Vector3 lastKnownRotation;
    private Vector3 lastKnownPosition;
    private Vector3 originalRootPos;
    private Vector3 originalPos;
    private const float moveThreshold = 0.02f;
    private const float turnThreshold = 0.4f;
    private float originalY;

    public bool IsWalking
    {
        get { return _animator.GetBool("IsWalking"); }
        set { _animator.SetBool("IsWalking", value); }
    }

    public bool IsJumping
    {
        get { return _animator.GetBool("IsJumping"); }
        set { _animator.SetBool("IsJumping", value); }
    }

    public bool IsStrafingLeft
    {
        get { return _animator.GetBool("IsStrafingLeft"); }
        set { _animator.SetBool("IsStrafingLeft", value); }
    }

    public bool IsStrafingRight
    {
        get { return _animator.GetBool("IsStrafingRight"); }
        set { _animator.SetBool("IsStrafingRight", value); }
    }

    public bool IsTurningLeft
    {
        get { return _animator.GetBool("IsTurningLeft"); }
        set { _animator.SetBool("IsTurningLeft", value); }
    }

    public bool IsTurningRight
    {
        get { return _animator.GetBool("IsTurningRight"); }
        set { _animator.SetBool("IsTurningRight", value); }
    }

    void Awake()
    {
        _animator = GetComponent<Animator>();
        lastKnownRotation = this.gameObject.transform.root.rotation.eulerAngles;
        originalRootPos = this.gameObject.transform.root.position;
        originalY = originalRootPos.y;
        originalPos = this.gameObject.transform.position;
    }

    void Update()
    {
       // this.gameObject.transform.position = originalPos;
        transformCharacterByAnimatingBooleans();
    }

    void transformCharacterByAnimatingBooleans()
    {
        if (!checkTurning())
        {
            checkWalking();
        }
        if (!(IsTurningLeft || IsTurningRight || IsWalking))
        {
            //hardResetY();
        }
    }

    bool checkTurning()
    {
        bool turned = false;
        Vector3 currentRotation = this.gameObject.transform.root.rotation.eulerAngles;
        float yDiff = lastKnownRotation.y - currentRotation.y;
        if (lastKnownRotation != currentRotation && Mathf.Abs(yDiff) > turnThreshold)
        {
            if (yDiff > 0)
            {
                IsTurningLeft = true;
            }
            else
            {
                IsTurningRight = true;
            }
            turned = true;
        }
        else
        {
            IsTurningRight = false;
            IsTurningLeft = false;
        }
        lastKnownRotation = currentRotation;
        return turned;
    }

    bool checkWalking()
    {
        bool walked = false;
        Vector3 currentPos = this.gameObject.transform.root.position;
        float xDiff = lastKnownPosition.x - currentPos.x;
        float zDiff = lastKnownPosition.z - currentPos.z;
        if (lastKnownPosition != currentPos && (Mathf.Abs(xDiff) > moveThreshold || Mathf.Abs(zDiff) > moveThreshold))
        {
            if (zDiff > 0)
            {
                IsWalking = true;
            }
            else if (zDiff < 0)
            {
                IsTurningLeft = true;
            }
            if (xDiff > 0)
            {
                IsStrafingRight = true;
            }
            else if (xDiff < 0)
            {
                IsStrafingLeft = true;
            }
            walked = true;
        }
        else
        {
            IsStrafingLeft = false;
            IsStrafingRight = false;
            IsTurningLeft = false;
            IsWalking = false;
        }
        lastKnownPosition = currentPos;
        return walked;
    }

    void hardResetY()
    {
        Vector3 currentPos = this.gameObject.transform.root.position;
        this.gameObject.transform.root.position = new Vector3(currentPos.x, originalY, currentPos.z);
    }

    void deleteGameObject()
    {
        Destroy(this.gameObject);
    }
}
