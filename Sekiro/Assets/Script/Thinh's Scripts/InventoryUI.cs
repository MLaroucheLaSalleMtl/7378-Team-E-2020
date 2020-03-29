using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private Transform itemParent = null;
    private Inventory inventory;
    private InventorySlot[] slots;
    

    // Start is called before the first frame update
    void Start()
    {
        inventory = Inventory.instance;
        inventory.onItemChangeCallBack += UpdateUI;
        slots = itemParent.GetComponentsInChildren<InventorySlot>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void UpdateUI()
    {
        for(int i =0;i < slots.Length; i++)
        {
            if (i < inventory.GetItemList().Count)
            {
                slots[i].AddItem(inventory.GetItemList()[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }
}
