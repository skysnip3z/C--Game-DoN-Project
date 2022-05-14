using System.Collections;
using UnityEngine;

public class WeaponSwing : MonoBehaviour
{
    public Animator anim;
    public AudioClip swingSound;
    private bool swinging;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && !swinging) 
        {
            StartCoroutine("Swing");
        }
    }

    IEnumerator Swing() 
    {
        swinging = true;
        transform.GetComponent<AudioSource>().PlayOneShot(swingSound);
        anim.SetTrigger("Swing");
        yield return new WaitForSeconds(0.3f);
        anim.SetTrigger("Idle");
        swinging = false;    
    }
}
