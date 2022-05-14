using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetStats : MonoBehaviour
{
    public void ResetStatsParent() 
    {
        gameObject.GetComponentInParent<UpgradesManager>().ResetStats();
    }
}
