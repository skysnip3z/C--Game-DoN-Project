using UnityEngine;
using UnityEngine.UI;

public class ToughText : MonoBehaviour
{
    public Text toughText;
    private int toughness;

    public void Update()
    {
        toughness = gameObject.GetComponentInParent<UpgradesManager>().toughness;
        toughText.text = toughness.ToString();
    }
}
