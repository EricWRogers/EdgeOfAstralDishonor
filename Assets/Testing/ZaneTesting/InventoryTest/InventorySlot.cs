using UnityEngine;
using UnityEngine.UI;
public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public Button removeButton;

    InventoryItemData item;

    public void Additem (InventoryItemData newItem)
    {
        item = newItem;

        icon.sprite = item.icon;
        icon.enabled = true;
        removeButton.interactable = true;

        Inventory.instance.Add(item);
    }

    public void ClearSlot ()
    {
        item = null;

        icon.sprite = null;
        icon.enabled = false;
        removeButton.interactable = false;
    }

    public void OnRemoveButton ()
    {
        Inventory.instance.Remove(item);
    }

    public void UseItem()
    {
        if (item != null)
        {
            item.Use();
        }
    }
}
