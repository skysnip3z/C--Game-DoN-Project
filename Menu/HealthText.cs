using UnityEngine;
using UnityEngine.UI;

public class HealthText : MonoBehaviour
{
    public Text healthText;
    private int maxHealth;

    public void Update()
    {
        maxHealth = gameObject.GetComponentInParent<UpgradesManager>().maxHealth;
        healthText.text = maxHealth.ToString();
    }
}
