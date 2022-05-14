using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LootPickup : MonoBehaviour
{
    public AudioClip pickUp;
    private Text text;

    private void Start()
    {
        text = GameObject.FindGameObjectWithTag("Message").GetComponent<Text>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") 
        {
            if (gameObject.tag == "Toughness")
            {
                text.SendMessage("DisplayMsg", "Toughness +2");
                other.gameObject.GetComponent<PlayerStats>().toughness += 2;
            }
            else if (gameObject.tag == "Maxhealth")
            {
                text.SendMessage("DisplayMsg", "Max Health +10");
                other.gameObject.GetComponent<PlayerStats>().maxHealth += 10;
                other.gameObject.GetComponent<PlayerStats>().UpdateMaxHealth();
            }
            else if (gameObject.tag == "Attackpower")
            {
                text.SendMessage("DisplayMsg", "Attack Power +10");
                other.gameObject.GetComponent<PlayerStats>().attackPower += 10;
            }
            else if (gameObject.tag == "NarikPoint")
            {
                int pointsAmount = GetNarikPointValue();
                string msg = "Narik Points +" + pointsAmount.ToString();
                text.SendMessage("DisplayMsg", msg);
                other.gameObject.GetComponent<PlayerStats>().narikPoints += pointsAmount;
                other.gameObject.SendMessage("UpdateNarikPoints");
            }
            StartCoroutine("PickUp");
        }
    }

    private int GetNarikPointValue() 
    {
        int difficulty = transform.parent.parent.gameObject.GetComponent<RoomManager>().difficultyLevel;

        if (difficulty > 0 && difficulty < 6)
            return 1;
        else if (difficulty > 5 && difficulty < 16)
            return 2;
        else if (difficulty > 15 && difficulty < 26)
            return 3;
        else if (difficulty > 25 && difficulty < 36)
            return 4;
        else if (difficulty > 35 && difficulty < 46)
            return 5;
        else if (difficulty > 45)
            return 6;
        else 
            return 1;
    }

    IEnumerator DelayedStart() 
    {
        yield return new WaitForSeconds(2.0f);
    
    }

    IEnumerator PickUp()
    {
        gameObject.GetComponent<AudioSource>().PlayOneShot(pickUp);
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
