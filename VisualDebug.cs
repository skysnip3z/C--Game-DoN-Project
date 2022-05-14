using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualDebug : MonoBehaviour
{
    private bool roofsRemoved = false;

    // Update is called once per frame
    void Update()
    {
        if (!roofsRemoved) 
        {
            GameObject[] roofs = GameObject.FindGameObjectsWithTag("RoofDebug");

            foreach (GameObject roof in roofs) 
            {
                roof.SetActive(false);
            }
            roofsRemoved = true;
        }
    }
} 
