using UnityEngine;
using UnityEngine.UI;

public class AttackBtn : MonoBehaviour
{
    public Button attackBtn;
    private int atkPower;
    private int narikPoints;

    public void Update()
    {
        atkPower = gameObject.GetComponentInParent<UpgradesManager>().attackPower;
        narikPoints = gameObject.GetComponentInParent<UpgradesManager>().narikPoints;
        if (atkPower >= 1000 || narikPoints <= 0)
        {
            attackBtn.enabled = false;
        }
        else
        {
            attackBtn.enabled = true;
        }
    }
}
