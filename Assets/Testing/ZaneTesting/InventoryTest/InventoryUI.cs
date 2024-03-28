using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Transform itemsParent;
    public GameObject inventoryUI;
    
    InventorySlot[] slots;
    private bool show = false;

    // Start is called before the first frame update
    void Start()
    { 
        itemsParent.gameObject.SetActive(false);

        InventorySystem.Instance.onItemChangedCallBack += UpdateUI;

        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
    }

    // Update is called once per frame
    void Update()
    {
        if(show)
        {
            UpdateUI();
        }
    }

    void PostPlay()
    {
        itemsParent.gameObject.SetActive(true);
        show = true;
    }

    void UpdateUI ()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < InventorySystem.Instance.inventory.Count)
            {
                if(slots[i].icon.enabled == false)
                {
                    slots[i].icon.enabled = true;
                }

                slots[i].Additem(InventorySystem.Instance.inventory[i].Data);
            }
            else
            {
                slots[i].ClearSlot();
                slots[i].icon.enabled = false;
            }
        }
    }
}
