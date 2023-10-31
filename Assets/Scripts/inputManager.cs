using UnityEngine;
using UnityEngine.InputSystem;

public class TimeController : MonoBehaviour
{
    public InputActionAsset actionAsset;

    private InputAction timeScaleAction;
    private InputAction skyboxAction;
    private InputAction volumeAction;

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
        this.transform.GetComponent<AudioSource>().pitch = 0.5f + value;
    }

    private void skyboxInputChanged(InputAction.CallbackContext context)
    {
        float value = context.ReadValue<float>();

        this.transform.GetComponent<skyboxDynamics>().skyBoxInterp = value;
    }

    private void volumeInputChanged(InputAction.CallbackContext context)
    {
        float value = context.ReadValue<float>();

        this.transform.GetComponent<AudioSource>().volume = value;
    }

    private void chickenAnimAInputChanged(InputAction.CallbackContext context)
    {
        float value = context.ReadValue<float>();

        this.transform.GetComponent<chickenManager>().mainChickenObject.GetComponent<Animator>().SetFloat("ChickenDance", value);
        Debug.Log(this.transform.GetComponent<chickenManager>().mainChickenObject.GetComponent<Animator>().GetFloat("ChickenDance"));
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
