using UnityEngine.InputSystem;
using UnityEngine;

public class EquipItem : MonoBehaviour
{
    public enum CurrentEquip
    {
        None,
        Kunai,
        Shuriken
    }

    public CurrentEquip currentEquip = CurrentEquip.None;
    private Inventory inventory;
    private PlayerController pc;

    public void OnEquip(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (currentEquip == CurrentEquip.None)
            {
                if (inventory.GetShurikenCount() > 0)
                {
                    currentEquip = CurrentEquip.Shuriken;
                }
                else if (inventory.GetKunaiCount() > 0)
                {
                    currentEquip = CurrentEquip.Kunai;
                }
            }
            else if(currentEquip == CurrentEquip.Shuriken)
            {
                if (inventory.GetKunaiCount() > 0)
                {
                    currentEquip = CurrentEquip.Kunai;
                }
                else
                {
                    currentEquip = CurrentEquip.None;
                }
            }
            else if(currentEquip == CurrentEquip.Kunai)
            {
                currentEquip = CurrentEquip.None;
            }
            
        }
    }

    public void OnHeal(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (inventory.GetPotionCount() > 0)
            {
                if (pc.HealSkill())
                {
                    inventory.RemovePotion();
                }
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        inventory = Inventory.Instance;
        currentEquip = CurrentEquip.None;
        pc = GetComponent<PlayerController>();
    }

    public CurrentEquip GetCurrentEquip() => currentEquip;

    private void Update()
    {
        ResetCurrentEquip();
    }

    private void ResetCurrentEquip()
    {
        if(currentEquip == CurrentEquip.Shuriken)
        {
            if (inventory.GetShurikenCount() <= 0)
            {
                currentEquip = CurrentEquip.None;
            }
        }
        else if(currentEquip == CurrentEquip.Kunai)
        {
            if (inventory.GetKunaiCount() <= 0)
            {
                currentEquip = CurrentEquip.None;
            }
        }
    }

    public void UseEquip()
    {
        if(currentEquip == CurrentEquip.Shuriken)
        {
            inventory.RemoveShuriken();
        }
        else if (currentEquip == CurrentEquip.Kunai)
        {
            inventory.RemoveKunai();
        }
    }

    public void ReturnToNone()
    {
        if(currentEquip != CurrentEquip.None)
        {
            currentEquip = CurrentEquip.None;
        }
    }
}
