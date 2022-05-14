using UnityEngine;

public class TidyObject : MonoBehaviour
{
    public float removeTime = 30.0f;

    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.tag != "NarikPoint") 
        {
            Destroy(gameObject, removeTime);
        } 
    }

    // DEBUG: stopping from editor
    private void OnApplicationQuit()
    {
        Destroy(gameObject);
    }
}
