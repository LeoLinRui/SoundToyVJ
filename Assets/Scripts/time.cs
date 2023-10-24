using UnityEngine;
using UnityEngine.InputSystem;

public class TimeController : MonoBehaviour
{
    public InputActionAsset actionAsset;  // Drag your Input Actions asset here in the Inspector
    public AudioSource audioSource;
    private InputAction control77Action;

    private void Awake()
    {
        // Assuming your action map's name is "Sliders"
        InputActionMap slidersMap = actionAsset.FindActionMap("Sliders");

        // From the map, get the specific action. Assuming it's named "A"
        control77Action = slidersMap.FindAction("A");

        if (control77Action != null)
        {
            control77Action.performed += OnControl77Performed;
        }
    }

    private void OnControl77Performed(InputAction.CallbackContext context)
    {
        float value = context.ReadValue<float>();
        Debug.Log(value);
        Time.timeScale = 0.5f + value;
        audioSource.pitch = 0.5f + value;
    }

    private void OnEnable()
    {
        //control77Action?.Enable();
    }

    private void OnDisable()
    {
        //control77Action?.Disable();
    }
}
