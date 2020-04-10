using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimManager : MonoBehaviour
{
    private AutoAim autoAim;
    private List<GameObject> aimPoints = new List<GameObject>();
    [SerializeField] private float maxRange = 10;
    [SerializeField] private GameObject aimUI;
    [SerializeField] private Vector3 offsetUI;
    private GameObject closestEnemy;
    // Start is called before the first frame update
    void Start()
    {
        autoAim = GetComponentInChildren<AutoAim>();
        aimUI.SetActive(false);
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("AimPoint");
        foreach(GameObject enemy in enemies)
        {
            aimPoints.Add(enemy);
        }
    }

    // Update is called once per frame
    void Update()
    {
        ClosestEnemy();
    }

    private void ClosestEnemy()
    {
        float range = maxRange;
        foreach(GameObject aimPoint in aimPoints)
        {
            float distance = Vector3.Distance(aimPoint.transform.position, transform.position);
            if(distance < range)
            {
                range = distance;
                closestEnemy = aimPoint;
            }
        }
        if (closestEnemy != null)
        {
            autoAim.SetTarget(closestEnemy);
            aimUI.transform.parent = closestEnemy.transform;
            aimUI.transform.position = closestEnemy.transform.position + offsetUI;
        }
    }

    public void ActiveAim()
    {
        aimUI.SetActive(true);
    }

    public void DeactiveAim()
    {
        aimUI.SetActive(false);
    }
}
