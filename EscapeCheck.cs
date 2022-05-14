using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EscapeCheck : MonoBehaviour
{
    private Text text;
    private bool escaped = false;

    void Start()
    {
        text = GameObject.FindGameObjectWithTag("Message").GetComponent<Text>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !escaped) 
        {
            escaped = true;
            text.color = Color.red;
            text.SendMessage("DisplayMsg", "You Have Escaped!");
            StartCoroutine("ReturnToMenu");
        }
    }

    IEnumerator ReturnToMenu() 
    {
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene(0);
    }
}
