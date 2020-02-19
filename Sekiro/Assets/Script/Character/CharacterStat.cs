using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class CharacterStat : MonoBehaviour
{
    private Stat _damage;
    private HealthSystem _healthSystem;
    private PostureSystem _postureSystem;
    public bool isAlive = true;

    public Stat Damage { get => _damage; set => _damage = value; }
    public HealthSystem HealthSystem { get => _healthSystem; set => _healthSystem = value; }
    public PostureSystem PostureSystem
    {
        get => _postureSystem; set => _postureSystem = value;
    }
}
