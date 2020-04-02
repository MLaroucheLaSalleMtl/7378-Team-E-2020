
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


}
