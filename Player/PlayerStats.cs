using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Create PlayerData to Save
public class PlayerStats : MonoBehaviour
{
    public int maxHealth;
    public int toughness;
    public int attackPower;
    public int narikPoints;
    public int currentHealth;

    public int startMaxHealth;
    public int startToughness;
    public int startAttackPower;

    public AudioClip hurtSound;
    public AudioClip dieSound;
    public HealthBar healthBar;
    public Text text;

    private bool isHurting = false;
    private bool dying = false;

    // Start is called before the first frame update
    void Start()
    {
        LoadStats();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    private void Update()
    {
        if (currentHealth <= 0 && !dying) 
        {
            dying = true;
            StartDeath();
        }
    }

    public void LoadStats()
    {
        SaveData stats = SaveSystem.LoadStats();
        if (!(stats == null))
        {
            maxHealth = stats.maxHealth;
            toughness = stats.toughness;
            attackPower = stats.attackPower;
            startMaxHealth = stats.maxHealth;
            startToughness = stats.toughness;
            startAttackPower = stats.attackPower;
            narikPoints = stats.narikPoints;
        }
        else
        {
            maxHealth = 100;
            toughness = 1;
            attackPower = 10;
            narikPoints = 0;
        }
    }

    // Taking toughness into account
    public void ReduceHealth(int dmg)
    {
        double reducedDmg = Math.Round((double)dmg - ((double)dmg * ((double)toughness / 100.0)), MidpointRounding.AwayFromZero);
        int newDmg = Convert.ToInt32(reducedDmg);
        if (!isHurting) 
        {
            StartCoroutine("PlayHurtSound");
        }
        currentHealth -= newDmg;
        healthBar.SetHealth(currentHealth);
    }

    public void UpdateMaxHealth()
    {
        healthBar.UpdateMaxHealth(maxHealth);
    }

    public void HealToFull()
    {
        currentHealth = maxHealth;
        healthBar.SetHealth(currentHealth);
    }

    public void UpdateNarikPoints()
    {
        PlayerSaveData data = new PlayerSaveData(
            startMaxHealth,
            startToughness,
            startAttackPower,
            narikPoints);
        SaveSystem.SaveStats(data);
    }

    private void StartDeath() 
    {
        StartCoroutine("Die");
    }

    IEnumerator PlayHurtSound() 
    {
        isHurting = true;
        transform.GetComponent<AudioSource>().PlayOneShot(hurtSound);
        yield return new WaitForSeconds(0.5f);
        isHurting = false;
    }

    IEnumerator Die() 
    {
        text.color = Color.red;
        text.SendMessage("DisplayMsg", "You Breathe Your Last....");
        transform.GetComponent<AudioSource>().PlayOneShot(dieSound);
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene(0);
    }
}
