using Random = System.Random;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject narikPoint;
    public Transform[] spawnPointsRight; // 8
    public Transform[] spawnPointsLeft; // 8
    public Transform spawnPointNarik;
    public int enemyCount;
    public int difficultyLevel;
    public bool playerInRoom;

    private bool narikSpawned = false;
    private Random rnd;

    // Start is called before the first frame update
    void Start()
    {
        PlaceEnemies();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyCount <= 0 && !narikSpawned)
        {
            SpawnNarikPoint();
            narikSpawned = true;
        }
        if (enemyCount > 0 && !playerInRoom)
        {
            DisableEnemies();
        }
        else if (enemyCount > 0 && playerInRoom) 
        {
            EnableEnemies();
        }

    }

    public void DecreaseEnemyCount()
    {
        if (enemyCount > 0)
        {
            enemyCount--;
        }
    }

    private void PlaceEnemies() 
    {
        bool flip = false;

        for (int i = 0; i < enemyCount; i++) 
        {
            GameObject enemy = Instantiate(enemyPrefab, transform.position,
                                    Quaternion.identity) as GameObject;
            enemy.name = "Enemy";
            enemy.transform.parent = transform.transform;
            if (flip)
            {
                Transform spawn = getRandomTransform(spawnPointsLeft, i);
                enemy.transform.position = spawn.position;
                enemy.GetComponent<EnemyController>().spawn = spawn;
                enemy.GetComponent<EnemyController>().destination = getRandomTransform(spawnPointsRight, i+3);
                flip = false;
            }
            else 
            {
                Transform spawn = getRandomTransform(spawnPointsRight, i);
                enemy.transform.position = spawn.position;
                enemy.GetComponent<EnemyController>().spawn = spawn;
                enemy.GetComponent<EnemyController>().destination = getRandomTransform(spawnPointsLeft, i+9);
                flip = true;
            }
        }
    }

    private void DisableEnemies() 
    {
        for (int i = 0; i < transform.childCount; i++) 
        {
            Transform t = transform.GetChild(i);
            if (t.gameObject.tag == "Enemy") 
            {
                t.gameObject.SetActive(false);
            }
        }
    }

    private void EnableEnemies()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform t = transform.GetChild(i);
            if (t.gameObject.tag == "Enemy")
            {
                t.gameObject.SetActive(true);
            }
        }
    } 

    private void SpawnNarikPoint()
    {
        GameObject point = Instantiate(narikPoint, transform.position,
                                    Quaternion.Euler(new Vector3(90, 0, 0))) as GameObject;
        point.name = "NarikPoint";
        point.transform.parent = transform.transform;
        point.transform.position = spawnPointNarik.position;
    }

    private Transform getRandomTransform(Transform[] arr, int seed) 
    {
        rnd = new Random(seed);
        int index = rnd.Next(0, arr.Length);
        return arr[index];
    }
}
