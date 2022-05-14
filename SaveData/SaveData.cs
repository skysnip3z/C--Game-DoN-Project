using UnityEngine;

[System.Serializable] // Be able to save to system
public class SaveData
{
    public int maxHealth;
    public int toughness;
    public int attackPower;
    public int narikPoints;

    public SaveData(PlayerSaveData stats) 
    {
        maxHealth = stats.maxHealth;
        toughness = stats.toughness;
        attackPower = stats.attackPower;
        narikPoints = stats.narikPoints;
    }
}
