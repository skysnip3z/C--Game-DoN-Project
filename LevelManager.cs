using Exception = System.Exception;
using System.Collections;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    /*
     * Prefabs Array:
     * [0] = Start Room
     * [1] = Middle Room
     * [2] = End Room
     */
    public GameObject[] prefabs = new GameObject[] { };
    public static int noOfRooms = 3;
    public float waitTime = 0.03f;
    public int enemyCount = 2;
    private Vector3 previous; 

    // Start is called before the first frame update
    void Start()
    {
        previous = transform.position;
        StartCoroutine("addRooms");
    }

    IEnumerator addRooms() 
    {
        for (int i = 0; i < noOfRooms; i++)
        {
            try
            {
                if (i == 0)
                {
                    GameObject start = Instantiate(prefabs[0], previous,
                                         Quaternion.identity) as GameObject;
                    start.name = "StartRoom";
                    start.GetComponent<RoomManager>().enemyCount = enemyCount;
                    start.GetComponent<RoomManager>().difficultyLevel = i + 1;
                    previous = new Vector3(0, 0, transform.position.z + 60);
                }
                else if (i < (noOfRooms - 1) && i > 0)
                {
                    GameObject middle = Instantiate(prefabs[1], previous,
                                         Quaternion.identity) as GameObject;
                    middle.name = "MiddleRoom";
                    middle.GetComponent<RoomManager>().enemyCount = enemyCount;
                    middle.GetComponent<RoomManager>().difficultyLevel = i + 1;
                    previous = new Vector3(0, 0, previous.z + 60);
                }
                else
                {
                    GameObject end = Instantiate(prefabs[2], previous,
                        Quaternion.identity) as GameObject;
                    end.name = "EndRoom";
                    end.GetComponent<RoomManager>().enemyCount = enemyCount + 1;
                    end.GetComponent<RoomManager>().difficultyLevel = i + 1;
                    previous += new Vector3(0, 0, previous.z + 60);
                }
            }
            catch (Exception e) 
            {
                // Log Trace
                Debug.Log(e.ToString());
            }
            yield return new WaitForSeconds(waitTime);
        }
    }
}
