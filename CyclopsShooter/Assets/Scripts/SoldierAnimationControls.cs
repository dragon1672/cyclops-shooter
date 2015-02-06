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

    public bool IsShooting
    {
        get { return _animator.GetBool("IsShooting"); }
        set { _animator.SetBool("IsShooting", value); }
    }

    public bool IsDead1
    {
        get { return _animator.GetBool("IsDead1"); }
        set { _animator.SetBool("IsDead1", value); }
    }

    public bool IsDead2
    {
        get { return _animator.GetBool("IsDead2"); }
        set { _animator.SetBool("IsDead2", value); }
    }

    public void setAllAnimationsFalse()
    {
        IsWalking = IsDead1 = IsDead2 = IsShooting = false;
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