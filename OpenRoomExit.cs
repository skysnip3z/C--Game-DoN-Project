using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class OpenRoomExit : MonoBehaviour
{
    public Animator anim;
    public AudioClip openSound;
    private GameObject roomManager;
    private int enemyCount;
    private Text text;
    private bool isOpen = false;

    private void Start()
    {
        text = GameObject.FindGameObjectWithTag("Message").GetComponent<Text>();
        roomManager = transform.parent.parent.gameObject;
        enemyCount = roomManager.GetComponent<RoomManager>().enemyCount;
    }

    void Update()
    {
        StartCoroutine("GetEnemyCount");
    }

    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") 
        {
            if (enemyCount <= 0 && !isOpen)
            {
                anim.SetTrigger("Open");
                transform.GetComponent<AudioSource>().PlayOneShot(openSound);
                isOpen = true;
                StartCoroutine("TidyObject");
            }
            else 
            {
                text.SendMessage("DisplayMsg", "Prehaps the enemies are bound to this door?");
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (enemyCount <= 0 && !isOpen)
            {
                anim.SetTrigger("Open");
                transform.GetComponent<AudioSource>().PlayOneShot(openSound);
                isOpen = true;
                StartCoroutine("TidyObject");
            }
        }
    }

    IEnumerator GetEnemyCount() 
    {
        yield return new WaitForSeconds(0.2f);
        enemyCount = roomManager.GetComponent<RoomManager>().enemyCount;
    }

    IEnumerator TidyObject() 
    {
        yield return new WaitForSeconds(8.0f);
        Destroy(gameObject);
    }
}
