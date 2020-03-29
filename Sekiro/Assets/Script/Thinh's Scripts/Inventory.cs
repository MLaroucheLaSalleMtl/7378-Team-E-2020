using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static Inventory instance = null;
    private List<Item> itemList;
    [SerializeField] private int space = 0;

    private void Awake()
    {
        if (instance != null)
        {
            return;
        }
        instance = this;
    }

    public delegate void OnItemChange();
    public OnItemChange onItemChangeCallBack;

    public List<Item> GetItemList() => itemList;

    public Inventory()
    {
        itemList = new List<Item>();
    }

    public bool AddItem(Item item)
    {
        if (itemList.Count >= space)
        {
            Debug.Log("Inventory is full!");
            return false;
        }
        
            itemList.Add(item);
        if (onItemChangeCallBack != null)
            {
                onItemChangeCallBack.Invoke();
            }
            return true;
        
        
    }

    public void RemoveItem(Item item)
    {
        itemList.Remove(item);
    }


}
