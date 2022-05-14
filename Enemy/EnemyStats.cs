using System;
using Random = System.Random;
using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class EnemyStats : MonoBehaviour
{
    public int maxHealth;
    public int attackPower;
    public int toughness;
    public int currentHealth;

    // [0] = Toughness Up, [1] = Max Health Up, [2] = Attack Power Up
    public GameObject[] lootTable;
    public HealthBar healthBar;
    public AudioClip enemyHurt;
    public AudioClip enemyDie;

    private Random rnd;
    private bool dying = false;
    private bool isHurt = false;
    private bool isScaled = false;
    private int difficultyLevel;

    // Start is called before the first frame update
    void Start()
    {
        difficultyLevel = transform.GetComponentInParent<RoomManager>().difficultyLevel;
        StartCoroutine("DelayedStart"); // Does not work on inactive enemies
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0 && dying == false)
        {
            dying = true;
            StartCoroutine("DropLoot");
        }
        if (!isScaled) 
        {
            isScaled = true;
            StartCoroutine("DelayedStart"); // For enemies that are set back to active
        }
    }

    // Taking toughness into account
    public void ReduceHealth(int dmg)
    {
        double reducedDmg = Math.Round((double)dmg - ((double)dmg * ((double)toughness / 100.0)), MidpointRounding.AwayFromZero);
        int newDmg = Convert.ToInt32(reducedDmg);
        StartCoroutine("PlayHurtSound");
        currentHealth -= newDmg;
        healthBar.SetHealth(currentHealth);
    }

    private void ScaleHealth(int seed) 
    {
        maxHealth = 100 + (seed * 15);
        if (maxHealth > 850)
        {
            maxHealth = 850;
        }
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    private void ScaleToughness(int seed) 
    {
        toughness = seed;
        if (toughness > 50) 
        {
            toughness = 50;
        }
    }

    private void ScaleAtckPower(int seed) 
    {
        attackPower = seed * 10;
        if (attackPower > 500) 
        {
            attackPower = 500;
        }
    }

    IEnumerator DelayedStart() 
    {
        yield return new WaitForSeconds(0.2f);
        ScaleHealth(difficultyLevel);
        ScaleToughness(difficultyLevel);
        ScaleAtckPower(difficultyLevel); 
    }

    IEnumerator DropLoot() 
    {
        gameObject.GetComponent<EnemyController>().Dead();
        transform.GetComponent<AudioSource>().PlayOneShot(enemyDie);
        yield return new WaitForSeconds(2.0f);
        rnd = new Random();
        int chance = rnd.Next(0, 3);
        GameObject loot = Instantiate(lootTable[chance], gameObject.transform.position,
                                        Quaternion.Euler(new Vector3(90, 0, 0))) as GameObject;
        loot.name = "loot";
        GetComponentInParent<RoomManager>().DecreaseEnemyCount();
        Destroy(gameObject);
    }

    IEnumerator PlayHurtSound() 
    {
        isHurt = true;
        transform.GetComponent<AudioSource>().PlayOneShot(enemyHurt);
        yield return new WaitForSeconds(0.2f);
        isHurt = false;
    }
}
