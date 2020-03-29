﻿
using UnityEngine;

public class Item : MonoBehaviour
{
    public enum ItemType
    {
        Katana,
        Kunai,
        Shuriken,
        HealthPotion
    }

    [SerializeField] private ItemType itemType;
    [SerializeField] private Sprite itemIcon;

    public ItemType GetItemType() => itemType;

    public Sprite GetItemIcon() => itemIcon;

    public virtual void Use()
    {
        Debug.Log("Use " + itemType);
    }

    public void RemoveFromInventory()
    {
        Inventory.instance.RemoveItem(this);
    }
}
