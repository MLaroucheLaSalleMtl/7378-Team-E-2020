using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CharacterStat : MonoBehaviour
{
    [SerializeField]
    private int maxHealth;

    public Slider healthBar;

    public int currentHealth { get; set; }

    public NPCController controller;

    public Stat damage;
    public Stat armor;

    public bool alive;

     CharacterAudio char_audio;
    public void Start()
    {
        char_audio = GetComponent<CharacterAudio>();
        controller = GetComponent<NPCController>();
        healthBar.maxValue = maxHealth;
        healthBar.value = maxHealth;
        alive = true;
    }

    public void SetHp()
        => currentHealth = maxHealth;

    private void Awake()
        => SetHp();

    public int GetMaxHealth() => maxHealth;

    public void TakeDamage(int damage)
    {
        if (alive)
        {
            char_audio.DamageSFX();
            currentHealth -= armor.GetValue();
            damage = Mathf.Clamp(damage, 0, int.MaxValue);

            currentHealth -= damage;
            healthBar.value = currentHealth;

            if (currentHealth <= 0)
                Die();
        }
    }

    public virtual void SetHealthBar(Slider slider)
        => healthBar = slider;

    public void RefillHealthBar()
    {
        healthBar.value = currentHealth;
    }
    public virtual void Die()
    {
        char_audio.DieSFX();
        Debug.Log("Die");
    }

    public virtual void ExecutionDie()
    {
        currentHealth -= maxHealth/2;
        healthBar.value = currentHealth;
    }
}
