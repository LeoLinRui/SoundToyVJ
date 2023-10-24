using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Time : MonoBehaviour
{
    public InputActionMap Sliders;
    private InputAction control77Action;
    private float control77Value;

    private void Awake()
    {
        // Obtain the action from the action map
        control77Action = Sliders.FindAction("A");

        // Register the callback for the slider action
        if (control77Action != null)
        {
            control77Action.performed += OnControl77Performed;
        }
    }

    private void OnControl77Performed(InputAction.CallbackContext context)
    {
        control77Value = context.ReadValue<float>();
        // Now you can use control77Value in any way you need within your script
        Debug.Log(control77Value);
    }

    private void OnEnable()
    {
        control77Action?.Enable();
    }

    private void OnDisable()
    {
        control77Action?.Disable();
    }
}
