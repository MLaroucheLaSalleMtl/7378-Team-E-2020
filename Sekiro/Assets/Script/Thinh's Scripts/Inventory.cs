using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : BaseSingleton<Inventory>
{
    private List<Item> itemList = new List<Item>();
    private int potionCount = 1;
    private int kunaiCount = 1;
    private int shurikenCount = 1;

    public List<Item> GetItemList() => itemList;
    public int GetPotionCount() => potionCount;
    public int GetKunaiCount() => kunaiCount;
    public int GetShurikenCount() => shurikenCount;
    
    public void AddItem(Item item)
    {
        itemList.Add(item);
        if (item.GetItemType() == Item.ItemType.Kunai)
        {
            kunaiCount +=1;
        }

        if (item.GetItemType() == Item.ItemType.Shuriken)
        {
            shurikenCount+=1;
        }

        if (item.GetItemType() == Item.ItemType.HealthPotion)
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
