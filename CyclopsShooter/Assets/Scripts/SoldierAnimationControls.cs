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

    public bool IsShooting
    {
        get { return _animator.GetBool("IsShooting"); }
        set { _animator.SetBool("IsShooting", value); }
    }

    public bool IsDead
    {
        get { return _animator.GetBool("IsDead"); }
        set { _animator.SetBool("IsDead", value); }
    }

    public void setAllAnimationsFalse()
    {
        IsWalking = IsJumping = IsStrafingRight = IsStrafingLeft = IsTurningRight = IsTurningLeft = IsShooting = IsDead = false;
    }

    void Awake()
    {
        _animator = GetComponent<Animator>();
        lastKnownRotation = this.gameObject.transform.root.rotation.eulerAngles;
        originalRootPos = this.gameObject.transform.root.position;
        originalY = originalRootPos.y;
        originalPos = this.gameObject.transform.position;
    }
}