using UnityEngine.UI;
using UnityEngine;
using UnityEngine.InputSystem;

public class ThrowShuriken : MonoBehaviour
{
    [Header("Throw")]
    [SerializeField] private GameObject shurikenThrow = null;
    [SerializeField] private GameObject kunaiThrow = null;
    [SerializeField] private GameObject shurikenHold = null;
    [SerializeField] private GameObject kunaiHold = null;
    [SerializeField] private Transform throwPos = null;
    [SerializeField] private float throwForce = 50;
    [SerializeField] private Transform leftHoldPos = null;
    [SerializeField] private Transform rightHoldPos = null;
    
    [Space]
    [Header("Aim")]
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private float aimDistance = 15;
    [SerializeField] private Sprite defaultCrossHair = null;
    [SerializeField] private Sprite aimCrossHair = null;
    [SerializeField] private Image crossHair = null;
    private CharacterAnimation char_anim;
    private GameObject throwObject = null;
    private EquipItem equipItem;

    // Start is called before the first frame update
    void Start()
    {
        char_anim = GetComponent<CharacterAnimation>();
        char_anim.AnimationThrowKunai(false);
        crossHair.sprite = defaultCrossHair;
        equipItem = GetComponent<EquipItem>();
    }

    // Update is called once per frame
    void Update()
    {
        Aiming();
        EnableCrossHair();
        SetWeaponPos();
        Equip();
    }

    private void SetWeaponPos()
    {
        if (GetComponent<PlayerController>().GetCurrentPlayerState() == State.Unarmed)
        {
            shurikenHold.transform.parent = rightHoldPos;
            kunaiHold.transform.parent = rightHoldPos;
            shurikenHold.transform.position = rightHoldPos.position;
            kunaiHold.transform.position = rightHoldPos.position;
            shurikenHold.transform.rotation = rightHoldPos.rotation;
            kunaiHold.transform.rotation = rightHoldPos.rotation;
        }
        else if (GetComponent<PlayerController>().GetCurrentPlayerState() == State.SwordAttaching)
        {
            shurikenHold.transform.parent = leftHoldPos;
            kunaiHold.transform.parent = leftHoldPos;
            shurikenHold.transform.position = leftHoldPos.position;
            kunaiHold.transform.position = leftHoldPos.position;
            shurikenHold.transform.rotation = leftHoldPos.rotation;
            kunaiHold.transform.rotation = leftHoldPos.rotation;
        }
    }

    private void Aiming()
    {
        RaycastHit hit;
        if (Physics.Raycast(throwPos.position, throwPos.forward, out hit , aimDistance, enemyLayer))
        {
            crossHair.sprite = aimCrossHair;
        }
        else
        {
            crossHair.sprite = defaultCrossHair;
        }
    }

    public bool CheckWeapons()
    {
        if(kunaiHold.activeInHierarchy || shurikenHold.activeInHierarchy)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void EnableCrossHair()
    {
        if(CheckWeapons())
        {
            crossHair.gameObject.SetActive(true);
        }
        else
        {
            crossHair.gameObject.SetActive(false);
        }
    }

    public void ThrowKunai()
    {
        if (kunaiHold.activeInHierarchy)
        {
            throwObject = Instantiate(kunaiThrow, throwPos.position, throwPos.rotation);
        }

        if (shurikenHold.activeInHierarchy)
        {
            throwObject = Instantiate(shurikenThrow, throwPos.position, throwPos.rotation);
        }
        Rigidbody rb = throwObject.GetComponent<Rigidbody>();
        rb.velocity = throwObject.transform.forward * throwForce;
        throwObject = null;
    }


    public void Equip()
    {
        if (equipItem.GetCurrentEquip() == EquipItem.CurrentEquip.None)
        {
            shurikenHold.SetActive(false);
            kunaiHold.SetActive(false);
            char_anim.AnimationThrowKunai(false);
        }
        else if(equipItem.GetCurrentEquip() == EquipItem.CurrentEquip.Shuriken)
        {
            shurikenHold.SetActive(true);
            kunaiHold.SetActive(false);
            char_anim.AnimationThrowKunai(true);
        }
        else if(equipItem.GetCurrentEquip() == EquipItem.CurrentEquip.Kunai)
        {
            kunaiHold.SetActive(true);
            shurikenHold.SetActive(false);
            char_anim.AnimationThrowKunai(true);
        }

    }

    public void ThrowFinish()
    {
        kunaiHold.SetActive(false);
        shurikenHold.SetActive(false);
        equipItem.UseEquip();
        char_anim.AnimationThrowKunai(false);
    }
}
