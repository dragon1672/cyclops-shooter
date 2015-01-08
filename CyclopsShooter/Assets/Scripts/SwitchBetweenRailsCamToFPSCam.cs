using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SwitchBetweenRailsCamToFPSCam : MonoBehaviour {

    public GameObject FPSController;
    public GameObject FPSMainCamera;
    public bool isOnRails = true;
    private bool lastKnownCamIsRails;

    void Start()
    {
        lastKnownCamIsRails = isOnRails;
        setCamToRails(isOnRails);
    }
    void Update()
    {
        if (isOnRails != lastKnownCamIsRails)
        {
            lastKnownCamIsRails = isOnRails;
            setCamToRails(isOnRails);
        }
    }

    private void setCamToRails(bool setToOn)
    {
        if (setToOn)
        {
            FPSController.GetComponent<MouseLook>().enabled = false;
            FPSController.GetComponent<MoveToTarget>().enabled = true;
            FPSController.GetComponent<LookAtTarget>().enabled = true;
            FPSMainCamera.GetComponent<MouseLook>().enabled = false;
        }
        else
        {
            FPSController.GetComponent<MouseLook>().enabled = true;
            FPSController.GetComponent<MoveToTarget>().enabled = false;
            FPSController.GetComponent<LookAtTarget>().enabled = false;
            FPSMainCamera.GetComponent<MouseLook>().enabled = true;
        }
    }
}
