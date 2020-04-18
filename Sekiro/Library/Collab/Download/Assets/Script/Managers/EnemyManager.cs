using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyManager : BaseSingleton<EnemyManager>
{
    [SerializeField]
    private Transform enemyHolder;
    [SerializeField]
    private Transform[] enemies;
    [SerializeField]
    private GameObject boss;
    [SerializeField]
    private bool bossIsAlive;

    public GameObject stair;
    // Start is called before the first frame update
    void Start()
    {
        stair.SetActive(false);
        enemies = new Transform[enemyHolder.childCount];

        for (int i = 0; i < enemies.Length; i++)
            enemies[i] = enemyHolder.GetChild(i);

        boss = GameObject.FindGameObjectWithTag("Boss");
    }

    bool IsAllEnemyDisable()
    {
        //return true if all is disable;
        return true;
    }

    private void Update()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if(enemies.Length <= 0)
        {
            stair.SetActive(true);
        }

    }


    public bool BossIsAlive()
    {
        if (boss.GetComponent<CharacterStat>().alive)
            return bossIsAlive = true;
        else
            return bossIsAlive = false;
    }
}
