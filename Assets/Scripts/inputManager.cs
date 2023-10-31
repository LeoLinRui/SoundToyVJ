using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-98)]
public class TimeController : MonoBehaviour
{
    public InputActionAsset actionAsset;

    private InputAction timeScaleAction;
    private InputAction skyboxAction;
    private InputAction volumeAction;
    private InputAction warpAction;

    private InputAction chickenAnimActionA;
    private InputAction chickenAnimActionB;
    private InputAction chickenAnimActionC;
    private InputAction chickenAnimActionD;
    private InputAction chickenAnimActionE;
    private InputAction chickenAnimActionF;

    private void Awake()
    {
        InputActionMap slidersMap = actionAsset.FindActionMap("Sliders");

        timeScaleAction = slidersMap.FindAction("A");
        skyboxAction = slidersMap.FindAction("B");
        volumeAction = slidersMap.FindAction("C");
        warpAction = slidersMap.FindAction("D");

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
        if (warpAction != null)
        {
            warpAction.performed += warpInputChanged;
        }

        InputActionMap sendAMap = actionAsset.FindActionMap("Send A");
        // GetComponent<chickenManager>().mainChickenObject.transform.Find("Body").GetComponent<Animator>() = GetComponent<chickenManager>().mainChickenObject.transform.Find("Body").GetComponent<Animator>();

        chickenAnimActionA = sendAMap.FindAction("A");
        chickenAnimActionB = sendAMap.FindAction("B");
        chickenAnimActionC = sendAMap.FindAction("C");
        chickenAnimActionD = sendAMap.FindAction("D");
        chickenAnimActionE = sendAMap.FindAction("E");
        chickenAnimActionF = sendAMap.FindAction("F");

        if (chickenAnimActionA != null)
        {
            chickenAnimActionA.performed += chickenAnimAInputChanged;
        }
        if (chickenAnimActionB != null)
        {
            chickenAnimActionB.performed += chickenAnimBInputChanged;
        }
        if (chickenAnimActionC != null)
        {
            chickenAnimActionC.performed += chickenAnimCInputChanged;
        }
        if (chickenAnimActionD != null)
        {
            chickenAnimActionD.performed += chickenAnimDInputChanged;
        }
        if (chickenAnimActionE != null)
        {
            chickenAnimActionE.performed += chickenAnimEInputChanged;
        }
        if (chickenAnimActionF != null)
        {
            chickenAnimActionF.performed += chickenAnimFInputChanged;
        }
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
    private void warpInputChanged(InputAction.CallbackContext context)
    {
        float value = context.ReadValue<float>();

        GetComponent<VertexShaderController>().warpVal = value;
    }


    private void chickenAnimAInputChanged(InputAction.CallbackContext context)
    {
        float value = context.ReadValue<float>();

        GameObject mainChicken = GetComponent<chickenManager>().mainChickenObject;
        mainChicken.transform.Find("Body").GetComponent<Animator>().SetFloat("ChickenDance", value);
        Transform poopTB = mainChicken.transform.Find("Body").Find("poopChicken");
        
        Transform poopTA = mainChicken.transform.Find("poopChicken");
        if(poopTA) {
            poopTA.GetComponent<Animator>().SetFloat("ChickenDance", value);
        } else if(poopTB) {
            poopTB.GetComponent<Animator>().SetFloat("ChickenDance", value);
        }
    }

    private void chickenAnimBInputChanged(InputAction.CallbackContext context)
    {
        float value = context.ReadValue<float>();

        GameObject mainChicken = GetComponent<chickenManager>().mainChickenObject;
        mainChicken.transform.Find("Body").GetComponent<Animator>().SetFloat("Spin", value);
        Transform poopTA = mainChicken.transform.Find("poopChicken");
        Transform poopTB = mainChicken.transform.Find("Body").Find("poopChicken");
        if(poopTA) {
            poopTA.GetComponent<Animator>().SetFloat("Spin", value);
        } else if(poopTB) {
            poopTB.GetComponent<Animator>().SetFloat("Spin", value);
        }
    }

    private void chickenAnimCInputChanged(InputAction.CallbackContext context)
    {
        float value = context.ReadValue<float>();

        GameObject mainChicken = GetComponent<chickenManager>().mainChickenObject;
        mainChicken.transform.Find("Body").GetComponent<Animator>().SetFloat("Stepping", value);
        Transform poopTA = mainChicken.transform.Find("poopChicken");
        Transform poopTB = mainChicken.transform.Find("Body").Find("poopChicken");
        if(poopTA) {
            poopTA.GetComponent<Animator>().SetFloat("Stepping", value);
        } else if(poopTB) {
            poopTB.GetComponent<Animator>().SetFloat("Stepping", value);
        }
    }

    private void chickenAnimDInputChanged(InputAction.CallbackContext context)
    {
        float value = context.ReadValue<float>();

        GameObject mainChicken = GetComponent<chickenManager>().mainChickenObject;
        mainChicken.transform.Find("Body").GetComponent<Animator>().SetFloat("Twerk", value);
        Transform poopTA = mainChicken.transform.Find("poopChicken");
        Transform poopTB = mainChicken.transform.Find("Body").Find("poopChicken");
        if(poopTA) {
            poopTA.GetComponent<Animator>().SetFloat("Twerk", value);
        } else if(poopTB) {
            poopTB.GetComponent<Animator>().SetFloat("Twerk", value);
        }
    }

    private void chickenAnimEInputChanged(InputAction.CallbackContext context)
    {
        float value = context.ReadValue<float>();

        GameObject mainChicken = GetComponent<chickenManager>().mainChickenObject;
        mainChicken.transform.Find("Body").GetComponent<Animator>().SetFloat("Twist", value);
        Transform poopTA = mainChicken.transform.Find("poopChicken");
        Transform poopTB = mainChicken.transform.Find("Body").Find("poopChicken");
        if(poopTA) {
            poopTA.GetComponent<Animator>().SetFloat("Twist", value);
        } else if(poopTB) {
            poopTB.GetComponent<Animator>().SetFloat("Twist", value);
        }
    }

    private void chickenAnimFInputChanged(InputAction.CallbackContext context)
    {

        float value = context.ReadValue<float>();
        GameObject mainChicken = GetComponent<chickenManager>().mainChickenObject;
        mainChicken.transform.Find("Body").GetComponent<Animator>().SetFloat("Salsa", value);
        Transform poopTA = mainChicken.transform.Find("poopChicken");
        Transform poopTB = mainChicken.transform.Find("Body").Find("poopChicken");
        if(poopTA) {
            poopTA.GetComponent<Animator>().SetFloat("Salsa", value);
        } else if(poopTB) {
            poopTB.GetComponent<Animator>().SetFloat("Salsa", value);
        }
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
