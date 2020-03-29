using UnityEngine.UI;
using UnityEngine;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] private Image icon = null;
    [SerializeField] private PlayerController player =null;
    private Item item;
    private bool isUsed = false;

    public void AddItem(Item newItem)
    {
        item = newItem;
        icon.sprite = item.GetItemIcon();
        icon.enabled = true;
    }

    public void ClearSlot()
    {
        Inventory.instance.RemoveItem(item);
        item = null;
        icon.sprite = null;
        icon.enabled = false;
    }

    public void UseItem()
    {
        item.Use();
        if (item.GetItemType() == Item.ItemType.HealthPotion)
        {
            isUsed = player.GetComponent<PlayerController>().HealSkill();
        }
        else
        {
            isUsed = player.GetComponent<ThrowShuriken>().Equip(item);
        }
        if (isUsed)
        {
            ClearSlot();
        }
        
    }
}
