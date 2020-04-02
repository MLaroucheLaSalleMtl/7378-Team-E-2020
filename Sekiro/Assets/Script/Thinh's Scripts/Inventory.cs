using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static Inventory instance = null;
    private List<Item> itemList;
    private int potionCount = 0;
    private int kunaiCount = 0;
    private int shurikenCount = 0;

    private void Awake()
    {
        if (instance != null)
        {
            return;
        }
        instance = this;
    }

    public List<Item> GetItemList() => itemList;
    public int GetPotionCount() => potionCount;
    public int GetKunaiCount() => kunaiCount;
    public int GetShurikenCount() => shurikenCount;

    public Inventory()
    {
        itemList = new List<Item>();
    }

    public void AddItem(Item item)
    {
        itemList.Add(item);
        if (item.GetItemType() == Item.ItemType.Kunai)
        {
            kunaiCount+=1;
        }
        else if (item.GetItemType() == Item.ItemType.Shuriken)
        {
            shurikenCount+=1;
        }
        else if (item.GetItemType() == Item.ItemType.HealthPotion)
        {
            potionCount+=1;
        }
    }

    public void RemoveKunai()
    {
        if(kunaiCount > 0)
        {
            kunaiCount -= 1;
        }
    }

    public void RemoveShuriken()
    {
        if (shurikenCount > 0)
        {
            shurikenCount -= 1;
        }
    }

    public void RemovePotion()
    {
        if (potionCount > 0)
        {
            potionCount -= 1;
        }
    }
}
