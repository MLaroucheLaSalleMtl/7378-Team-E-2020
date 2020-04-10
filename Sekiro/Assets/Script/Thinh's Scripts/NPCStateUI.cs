using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCStateUI : MonoBehaviour
{
    [SerializeField] private GameObject pCautionUI = null;
    [SerializeField] private GameObject pChaseUI = null;
    [SerializeField] private Transform pos = null;

    private AIController state;
    private GameObject cautionUI;
    private GameObject chaseUI;
    // Start is called before the first frame update
    void Start()
    {
        state = GetComponent<AIController>();
        cautionUI = Instantiate(pCautionUI, pos.position, pos.rotation) as GameObject;
        chaseUI = Instantiate(pChaseUI, pos.position, pos.rotation) as GameObject;
        chaseUI.transform.parent = pos;
        cautionUI.transform.parent = pos;
    }

    // Update is called once per frame
    void Update()
    {
        if (state.GetCurrentState() == NPCState.Caution)
        {
            cautionUI.SetActive(true);
            chaseUI.SetActive(false);
        }
        else if(state.GetCurrentState() == NPCState.Chase)
        {
            cautionUI.SetActive(false);
            chaseUI.SetActive(true);
        }
        else
        {
            cautionUI.SetActive(false);
            chaseUI.SetActive(false);
        }
    }
}
