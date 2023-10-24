using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class cameraController : MonoBehaviour
{
    public InputActionAsset actionAsset;
    public CinemachineVirtualCamera[] virtualCameras;

    private void Awake()
    {
        InputActionMap buttonMap = actionAsset.FindActionMap("Track Focus");

        Debug.Assert(virtualCameras.Length <= 8);

        for (int i = 0; i < virtualCameras.Length; i++)
        {
            int camIndex = i;  // Necessary to avoid "modified closure" problem
            buttonMap.actions[i].performed += SwitchToCamera(camIndex);
        }
    }

    private Action<InputAction.CallbackContext> SwitchToCamera(int index)
    {
        // This function returns another function (the callback to use)
        return (InputAction.CallbackContext context) =>
        {
            if (context.ReadValue<float>() > 0.5f) // Assuming button press sends a value of 1
            {
                foreach (var vCam in virtualCameras)
                {
                    vCam.Priority = 0;  // Set all cameras to default priority
                }
                virtualCameras[index].Priority = 1;  // Boost the priority of the chosen camera to make it active
            }
        };
    }

    private void OnEnable()
    {
        actionAsset.Enable();  // Assuming you want to enable the entire asset
    }

    private void OnDisable()
    {
        actionAsset.Disable();  // Assuming you want to disable the entire asset
    }
}
