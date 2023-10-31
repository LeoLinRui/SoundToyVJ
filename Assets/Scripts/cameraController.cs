using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-98)]
public class cameraController : MonoBehaviour
{
    public InputActionAsset actionAsset;
    public CinemachineVirtualCamera[] virtualSceneCameras;

    private CinemachineVirtualCamera[] virtualChickenCameras = new CinemachineVirtualCamera[8];
    private GameObject prevChicken;

    private void Start()
    {
        prevChicken = GetComponent<chickenManager>().mainChickenObject;
        Transform CMVCChicken = GetComponent<chickenManager>().mainChickenObject.transform.Find("CMVC");
        for (int i = 1; i <= 8; i++)
        {
            virtualChickenCameras[i - 1] = CMVCChicken.Find(i.ToString()).GetComponent<CinemachineVirtualCamera>();
        }

        InputActionMap buttonMapScene = actionAsset.FindActionMap("Track Focus");

        Debug.Assert(virtualSceneCameras.Length <= 8);

        for (int i = 0; i < virtualSceneCameras.Length; i++)
        {
            int camIndex = i;  // Necessary to avoid "modified closure" problem
            buttonMapScene.actions[i].performed += SwitchToSceneCamera(camIndex);
        }

        InputActionMap buttonMap = actionAsset.FindActionMap("Track Control");

        Debug.Assert(virtualChickenCameras.Length <= 8);

        for (int i = 0; i < virtualChickenCameras.Length; i++)
        {
            int camIndex = i;  // Necessary to avoid "modified closure" problem
            buttonMap.actions[i].performed += SwitchToChickenCamera(camIndex);
        }
    }

    private Action<InputAction.CallbackContext> SwitchToSceneCamera(int index)
    {
        // This function returns another function (the callback to use)
        return (InputAction.CallbackContext context) =>
        {
            if (context.ReadValue<float>() > 0.5f) // Assuming button press sends a value of 1
            {
                foreach (var vCam in virtualSceneCameras)
                {
                    vCam.Priority = 0;  // Set all cameras to default priority
                }
                foreach (var vCam in virtualChickenCameras)
                {
                    vCam.Priority = 0;  // Set all cameras to default priority
                }
                virtualSceneCameras[index].Priority = 1;  // Boost the priority of the chosen camera to make it active
            }
        };
    }

    private Action<InputAction.CallbackContext> SwitchToChickenCamera(int index)
    {
        // This function returns another function (the callback to use)
        return (InputAction.CallbackContext context) =>
        {
            if (context.ReadValue<float>() > 0.5f) // Assuming button press sends a value of 1
            {
                foreach (var vCam in virtualSceneCameras)
                {
                    vCam.Priority = 0;  // Set all cameras to default priority
                }
                foreach (var vCam in virtualChickenCameras)
                {
                    vCam.Priority = 0;  // Set all cameras to default priority
                }
                virtualChickenCameras[index].Priority = 1;  // Boost the priority of the chosen camera to make it active
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

    private void Update() {
        if (GetComponent<chickenManager>().mainChickenObject != prevChicken) {
            Transform CMVCChicken = GetComponent<chickenManager>().mainChickenObject.transform.Find("CMVC");
            for (int i = 1; i <= 8; i++)
            {
                virtualChickenCameras[i - 1] = CMVCChicken.Find(i.ToString()).GetComponent<CinemachineVirtualCamera>();
            }

            InputActionMap buttonMapScene = actionAsset.FindActionMap("Track Focus");

            Debug.Assert(virtualSceneCameras.Length <= 8);

            for (int i = 0; i < virtualSceneCameras.Length; i++)
            {
                int camIndex = i;  // Necessary to avoid "modified closure" problem
                buttonMapScene.actions[i].performed += SwitchToSceneCamera(camIndex);
            }

            InputActionMap buttonMap = actionAsset.FindActionMap("Track Control");

            Debug.Assert(virtualChickenCameras.Length <= 8);

            for (int i = 0; i < virtualChickenCameras.Length; i++)
            {
                int camIndex = i;  // Necessary to avoid "modified closure" problem
                buttonMap.actions[i].performed += SwitchToChickenCamera(camIndex);
            }
        } 
        prevChicken = GetComponent<chickenManager>().mainChickenObject;
    }
}
