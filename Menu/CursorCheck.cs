using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorCheck : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Cursor.lockState == CursorLockMode.Locked) 
        {
            Cursor.lockState = CursorLockMode.Confined;
        }     
    }
}
