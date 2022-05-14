using UnityEngine;

public class UpgradesManager : MonoBehaviour
{
    public int maxHealth;
    public int toughness;
    public int attackPower;
    public int narikPoints;

    // Start is called before the first frame update
    void Start()
    {
        LoadStats();
    }

    // Cheat: narikPoints + 1000
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.RightBracket))
        {
            narikPoints += 100;
        }
    }

    public void AddMaxHealth(int increase)
    {
        maxHealth += increase;
        UpdateStats();
    }

    public void AddAtkPower(int increase)
    {
        attackPower += increase;
        UpdateStats();
    }

    public void AddToughness(int increase)
    {
        toughness += increase;
        UpdateStats();
    }

    private void UpdateStats()
    {
        narikPoints -= 1;
        SaveStats();
        LoadStats();
    }

    public void ResetStats() 
    {
        PlayerSaveData data = new PlayerSaveData(100, 1, 10, 1);
        SaveSystem.SaveStats(data);
        LoadStats();
    }

    private void LoadStats()
    {
        SaveData stats = SaveSystem.LoadStats();
        if (!(stats == null))
        {
            maxHealth = stats.maxHealth;
            toughness = stats.toughness;
            attackPower = stats.attackPower;
            narikPoints = stats.narikPoints;
        }
        else
        {
            maxHealth = 100;
            toughness = 1;
            attackPower = 10;
            narikPoints = 1;
        }
    }

    private void SaveStats() 
    {
        PlayerSaveData data = new PlayerSaveData(maxHealth, toughness, attackPower, narikPoints);
        SaveSystem.SaveStats(data);
    }
}
