using UnityEngine;
using UnityEngine.UI;

public class HealthBtn : MonoBehaviour
{
    public Button healthBtn;
    private int maxHealth;
    private int narikPoints;

    public void Update()
    {
        maxHealth = gameObject.GetComponentInParent<UpgradesManager>().maxHealth;
        narikPoints = gameObject.GetComponentInParent<UpgradesManager>().narikPoints;
        if (maxHealth >= 1000 || narikPoints <= 0)
        {
            healthBtn.enabled = false;
        }
        else
        {
            healthBtn.enabled = true;
        }
    }
}
