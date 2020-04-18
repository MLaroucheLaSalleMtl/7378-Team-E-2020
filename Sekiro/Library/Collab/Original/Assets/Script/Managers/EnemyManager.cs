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

    // Start is called before the first frame update
    void Start()
    {
        enemies = new Transform[enemyHolder.childCount];

        for (int i = 0; i < enemies.Length; i++)
            enemies[i] = enemyHolder.GetChild(i);
        enemies.ToList();
        boss = GameObject.FindGameObjectWithTag("Boss");
    }

    public bool BossIsAlive()
    {
        if (boss.GetComponent<CharacterStat>().alive)
            return bossIsAlive = true;
        else
            return bossIsAlive = false;
    }
}
