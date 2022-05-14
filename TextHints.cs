using UnityEngine;
using UnityEngine.UI;

public class TextHints : MonoBehaviour
{
    private float timer = 0.0f;

    // Update is called once per frame
    void Update()
    {
        GetTimer();
    }

    public void DisplayMsg(string message)
    {
        gameObject.GetComponent<Text>().text = message;
        if (!gameObject.GetComponent<Text>().enabled)
        {
            gameObject.GetComponent<Text>().enabled = true;
            gameObject.GetComponent<CanvasGroup>().alpha = 1;
        }
    }

    private void GetTimer() 
    {
        if (gameObject.GetComponent<Text>().enabled)
        {
            timer += Time.deltaTime;
            if (timer >= 4)
            {
                gameObject.GetComponent<Text>().enabled = false;
                gameObject.GetComponent<CanvasGroup>().alpha = 0;
                timer = 0.0f;
            }
        }
    }
}
