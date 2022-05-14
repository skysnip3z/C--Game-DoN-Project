using UnityEngine;
using UnityEngine.UI;

public class AttackText : MonoBehaviour
{
    public Text atkText;
    private int attackPower;

    public void Update()
    {
        attackPower = gameObject.GetComponentInParent<UpgradesManager>().attackPower;
        atkText.text = attackPower.ToString();
    }
}
