using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnClick : MonoBehaviour
{
    public void setRoomCount(int noOfRooms) 
    {
        LevelManager.noOfRooms = noOfRooms;
    }
    public void LoadByIndex(int sceneIndex) 
    {
        StartCoroutine("DelayedLoad", sceneIndex);
    }

    IEnumerator DelayedLoad(int sceneIndex) 
    {
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene(sceneIndex);
    }
}
