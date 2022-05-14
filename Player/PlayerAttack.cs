using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private int attackPower;

    private void Update()
    {
        attackPower = gameObject.GetComponent<PlayerStats>().attackPower;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 4))
        {
            if (hit.transform.gameObject.tag == "Enemy") 
            {
                if (Input.GetKeyDown(KeyCode.Mouse0)) 
                {
                    hit.transform.gameObject.GetComponent<EnemyStats>().ReduceHealth(attackPower);
                }
            }
        }
    }

}
