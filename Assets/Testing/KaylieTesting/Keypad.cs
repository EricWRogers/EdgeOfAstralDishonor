using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class Keypad : Interactable
{
    public GameObject keypadUI;
    public Canvas canvas;
    public Locker door;

    public override void OnInteract()
    {
        base.OnInteract();

        var go = Instantiate(keypadUI, canvas.transform);
        go.transform.SetAsLastSibling();

        if (door != null && door.useKeypad)
        {
            go.GetComponent<KeypadUIController>().onCorrectPassword.AddListener(() => door.SetInteractable(true));
        }

        OmnicatLabs.CharacterControllers.CharacterController.Instance.SetControllerLocked(true, true, true);
    }

    protected override void OnHover()
    {
        base.OnHover();

        GetComponent<Dialogue>().TriggerDialogue();
    }
}