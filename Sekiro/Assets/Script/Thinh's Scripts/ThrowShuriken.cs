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
    private Item currentItem = null;
    private GameObject throwObject = null;

    // Start is called before the first frame update
    void Start()
    {
        char_anim = GetComponent<CharacterAnimation>();
        char_anim.AnimationThrowKunai(false);
        crossHair.sprite = defaultCrossHair;
    }

    // Update is called once per frame
    void Update()
    {
        Aiming();
        EnableCrossHair();
        SetWeaponPos();
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
        if (Physics.Raycast(throwPos.position, throwPos.up, out hit , aimDistance, enemyLayer))
        {
            crossHair.sprite = aimCrossHair;
        }
        else
        {
            crossHair.sprite = defaultCrossHair;
        }
    }

    private void EnableCrossHair()
    {
        if(kunaiHold.activeInHierarchy || shurikenHold.activeInHierarchy)
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
        rb.velocity = throwObject.transform.up * throwForce;
        throwObject = null;
    }


    public bool Equip(Item newItem)
    {
        currentItem = newItem;
        bool check=false;
        if (currentItem.GetItemType() == Item.ItemType.Shuriken)
        {
            if (shurikenHold.activeInHierarchy)
            {
                check = false; 
            }
            else
            {
                kunaiHold.SetActive(false);
                shurikenHold.SetActive(true);
                check = true;
            }
        }
        else if (currentItem.GetItemType() == Item.ItemType.Kunai)
        {
            if (kunaiHold.activeInHierarchy)
            {
                check = false;
            }
            else
            {
                shurikenHold.SetActive(false);
                kunaiHold.SetActive(true);
                check = true;
            }
        }
        char_anim.AnimationThrowKunai(true);
        return check;
    }

    public void ThrowFinish()
    {
        kunaiHold.SetActive(false);
        shurikenHold.SetActive(false);
        char_anim.AnimationThrowKunai(false);
    }
}
