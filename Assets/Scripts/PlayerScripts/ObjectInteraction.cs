using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using OmnicatLabs.Extensions;

public class ObjectInteraction : MonoBehaviour
{
    [SerializeField]
    [Tooltip("This is interactable UI text that you can edit with tweening.")]
    public TextMeshProUGUI interactText;

    [SerializeField]
    [Tooltip("This is where you add the camera you want the raycast to fire from.")]
    private new Camera camera;

    public float interactRange;
    public LayerMask interactableLayer;
    private string interactKeyName;
    private int bindingIndex;
    private Interactable interactableObject;

    void Start()
    {
        SetInteractTextVisibility(false);
        if (OmnicatLabs.CharacterControllers.CharacterController.Instance.GetComponent<PlayerInput>().currentControlScheme == "Controller")
        {
            bindingIndex = OmnicatLabs.CharacterControllers.CharacterController.Instance.GetComponent<PlayerInput>().actions["Interact"].GetBindingIndex(group: "Controller");
        }
        else
        {
            bindingIndex = OmnicatLabs.CharacterControllers.CharacterController.Instance.GetComponent<PlayerInput>().actions["Interact"].GetBindingIndex(group: "FPS MNK Controls");
        }
        interactKeyName = OmnicatLabs.CharacterControllers.CharacterController.Instance.GetComponent<PlayerInput>().actions["Interact"].GetBindingDisplayString(bindingIndex, out _, out _);

        OmnicatLabs.CharacterControllers.CharacterController.Instance.GetComponent<PlayerInput>().controlsChangedEvent.AddListener(DeviceChanged);
    }

    private void DeviceChanged(PlayerInput input)
    {
        if (OmnicatLabs.CharacterControllers.CharacterController.Instance.GetComponent<PlayerInput>().currentControlScheme == "Controller")
        {
            bindingIndex = OmnicatLabs.CharacterControllers.CharacterController.Instance.GetComponent<PlayerInput>().actions["Interact"].GetBindingIndex(group: "Controller");
        }
        else
        {
            bindingIndex = OmnicatLabs.CharacterControllers.CharacterController.Instance.GetComponent<PlayerInput>().actions["Interact"].GetBindingIndex(group: "FPS MNK Controls");
        }
        interactKeyName = OmnicatLabs.CharacterControllers.CharacterController.Instance.GetComponent<PlayerInput>().actions["Interact"].GetBindingDisplayString(bindingIndex, out _, out _);
    }


    void Update()
    {
        Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

        if (Physics.Raycast(ray, out RaycastHit hit, interactRange, interactableLayer))
        {
            //Interactable interactableObject = hit.collider.GetComponent<Interactable>();
            if (hit.transform.TryGetComponentInParentAndChildren(out interactableObject))
            {
                // Display interaction info. I.E. Tween things go here.
                //Debug.Log("Raycast has hit interactable object");
                Debug.DrawLine(ray.origin, hit.point, Color.red);

                interactableObject.onHover.Invoke();

                if (interactableObject.interactText != "" && interactableObject.canInteract)
                {
                    interactText.text = $"{interactKeyName} {interactableObject.interactText}";
                    SetInteractTextVisibility(true);
                }
                else if (interactableObject.uninteractableText != "" && !interactableObject.canInteract)
                {
                    interactText.SetText(interactableObject.uninteractableText);
                    SetInteractTextVisibility(true);
                }
            }
            else
            {
                SetInteractTextVisibility(false);
                if (interactText != null)
                    interactText.SetText("");
                else Debug.Log("No interact text object found. Make sure it is assigned in the inspector");
                interactableObject = null;
            }
        }
        else
        {
            // Hides the interaction text when not looking, or in range of the object
            SetInteractTextVisibility(false);
            interactableObject = null;
        }
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed && interactableObject != null && !OmnicatLabs.CharacterControllers.CharacterController.Instance.isLocked)
        {
            interactableObject.Interact();
        }
    }

    public void SetInteractTextVisibility(bool isVisable)
    {
        if (interactText != null)
        {
            interactText.enabled = isVisable;
        }
    }
}
