using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class TimeController : MonoBehaviour
{
    public InputActionAsset actionAsset;

    private InputAction timeScaleAction;
    private InputAction skyboxAction;
    private InputAction volumeAction;

    private Animator chickenAnimator;
    private InputAction chickenAnimActionA;
    private InputAction chickenAnimActionB;
    private InputAction chickenAnimActionC;

    private void Awake()
    {
        InputActionMap slidersMap = actionAsset.FindActionMap("Sliders");

        timeScaleAction = slidersMap.FindAction("A");
        skyboxAction = slidersMap.FindAction("B");
        volumeAction = slidersMap.FindAction("C");

        if (timeScaleAction != null)
        {
            timeScaleAction.performed += timeInputChanged;
        }
        if (skyboxAction != null)
        {
            skyboxAction.performed += skyboxInputChanged;
        }
        if (volumeAction != null)
        {
            volumeAction.performed += volumeInputChanged;
        }

        InputActionMap sendAMap = actionAsset.FindActionMap("Send A");
        chickenAnimator = GetComponent<chickenManager>().mainChickenObject.transform.Find("Body").GetComponent<Animator>();

        chickenAnimActionA = sendAMap.FindAction("A");
        chickenAnimActionB = sendAMap.FindAction("B");
        chickenAnimActionC = sendAMap.FindAction("C");

        if (chickenAnimActionA != null)
        {
            chickenAnimActionA.performed += chickenAnimAInputChanged;
        }

#if !UNITY_EDITOR
        Time.timeScale = 0;
#endif
    }

    private void timeInputChanged(InputAction.CallbackContext context)
    {
        float value = context.ReadValue<float>();

        Time.timeScale = 0.5f + value;
        GetComponent<AudioSource>().pitch = 0.5f + value;
    }

    private void skyboxInputChanged(InputAction.CallbackContext context)
    {
        float value = context.ReadValue<float>();

        GetComponent<skyboxDynamics>().skyBoxInterp = value;
    }

    private void volumeInputChanged(InputAction.CallbackContext context)
    {
        float value = context.ReadValue<float>();

        GetComponent<AudioSource>().volume = value;
    }

    private void chickenAnimAInputChanged(InputAction.CallbackContext context)
    {
        float value = context.ReadValue<float>();

        GetComponent<chickenManager>().mainChickenObject.transform.Find("Body").GetComponent<Animator>().SetFloat("ChickenDance", value);
    }

    private void chickenAnimBInputChanged(InputAction.CallbackContext context)
    {
        float value = context.ReadValue<float>();

        GetComponent<chickenManager>().mainChickenObject.transform.Find("Body").GetComponent<Animator>().SetFloat("Spin", value);
    }

    private void chickenAnimCInputChanged(InputAction.CallbackContext context)
    {
        float value = context.ReadValue<float>();

        GetComponent<chickenManager>().mainChickenObject.transform.Find("Body").GetComponent<Animator>().SetFloat("Spin", value);
    }

    private void OnEnable()
    {
        //timeScaleAction?.Enable();
    }

    private void OnDisable()
    {
        //timeScaleAction?.Disable();
    }
}
