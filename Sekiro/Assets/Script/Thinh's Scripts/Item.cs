
using UnityEngine;

public class Item : MonoBehaviour
{
    public enum ItemType
    {
        Kunai,
        Shuriken,
        HealthPotion
    }

    [SerializeField] private ItemType itemType;

    public ItemType GetItemType() => itemType;



}
