using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StealthKill : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private GameObject playerInput = null;
    private CharacterAnimation char_anim;
    private PlayerController player;
    private CharacterAudio char_audio;
    public bool canTakeDown = false;
    private EnemyStat enemyStat;
    private bool isFinish;

    public bool GetisFinish() => isFinish;
    
    // Start is called before the first frame update
    void Start()
    {
        char_anim = GetComponent<CharacterAnimation>();
        player = GetComponent<PlayerController>();
        char_audio = GetComponent<CharacterAudio>();
        isFinish = true;
    }

    // Update is called once per frame
    void Update()
    {
  
    }
    #region(Check Collision)
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("ExecutionArea"))
        {
            target = other.transform.parent;
            enemyStat = other.GetComponentInParent<EnemyStat>();
            if (player.GetCurrentPlayerState() == State.SwordAttaching)
            {
                canTakeDown = true;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("ExecutionArea"))
        {
            if (player.GetCurrentPlayerState() == State.SwordAttaching)
            {
                canTakeDown = true;
            }
            else
            {
                canTakeDown = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("ExecutionArea"))
        {
            canTakeDown = false;
            target = null;
        }
    }
    #endregion


    public void TakeDown()//to call in characterattack
    {
        target.LookAt(this.transform);
        transform.LookAt(target);
        char_anim.TakeDown(true);
    }


    public void StartTakeDown()
    {
        playerInput.SetActive(false);
        isFinish = false;
    }

    public void ExecutionKill1()//to call in take down animation event
    {
        if (enemyStat != null)
        {
            enemyStat.ExecutionDie1();
        }
        char_audio.NormalAttackSFX();
    }

    public void ExecutionKill2()//to call in take down animation event
    {
        if (enemyStat != null)
        {
            enemyStat.ExecutionDie2();
        }
        char_audio.SwordCut();
    }

    public void StopTakeDown()
    {
        char_anim.TakeDown(false);
        playerInput.SetActive(true);
        canTakeDown = false;
        isFinish = true;
    }
}
