using UnityEngine;
using UnityEngine.UI;

public class NarikText : MonoBehaviour
{
    public Text narikText;
    private int points;

    public void Update()
    {
        points = gameObject.GetComponentInParent<UpgradesManager>().narikPoints;
        narikText.text = points.ToString();
    }
}
