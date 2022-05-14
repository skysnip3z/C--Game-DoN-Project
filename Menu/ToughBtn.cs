using UnityEngine;
using UnityEngine.UI;

public class ToughBtn : MonoBehaviour
{
    public Button toughBtn;
    private int toughness;
    private int narikPoints;

    public void Update()
    {
        toughness = gameObject.GetComponentInParent<UpgradesManager>().toughness;
        narikPoints = gameObject.GetComponentInParent<UpgradesManager>().narikPoints;
        if (toughness >= 70 || narikPoints <= 0)
        {
            toughBtn.enabled = false;
        }
        else 
        {
            toughBtn.enabled = true;
        }
    }
}
