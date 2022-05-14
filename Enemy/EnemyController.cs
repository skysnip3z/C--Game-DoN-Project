using Random = System.Random;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public Transform spawn;
    public Transform destination;
    public float attackSpeed; // 3 default
    public float findRange; // 8 Default
    public float attackRange; // 3 Default
    public float dodgeTime; // 0.4 Second Default
    public int attackRate; // 1:15 Optimal
    public Animator[] swords;
    public AudioClip enemyAtk;

    private GameObject player;
    private NavMeshAgent agent;
    private Random rnd = new Random();
    private bool findPlayer;
    private bool toDest = false;
    private bool attacking;
    private bool isDead = false;
    private bool reactivated = false;
    private int attackPower;
    private float distanceFromPlayer = 1000.0f;



    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = false;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        // Due to deactivating for performance 
        if (!reactivated) 
        {
            StartCoroutine("DelayedStart");
            reactivated = true;
        }

        findPlayer = GetComponentInParent<RoomManager>().playerInRoom;


        if (!findPlayer)
        {
            if (!agent.pathPending && agent.remainingDistance < 3.0f)
            {
                GoToNextDest();
            }
        }
        else {
            if (!agent.pathPending && agent.remainingDistance < 3.0f)
            {
                GoToNextDest();
            }
            distanceFromPlayer = Vector3.Distance(player.transform.position, transform.position);
        }

        if (findPlayer && distanceFromPlayer <= findRange && distanceFromPlayer >= attackRange)
        {
            agent.SetDestination(player.transform.position);
        }
        else if (findPlayer && distanceFromPlayer < findRange)
        {
            agent.SetDestination(player.transform.position);
        }
    }

    public void Dead() 
    {
        isDead = true;
    }

    private void GoToNextDest() 
    {
        if (!toDest)
        {
            agent.SetDestination(destination.position);
            toDest = true;
        }
        else 
        {
            agent.SetDestination(spawn.position);
            toDest = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        ChanceToHit(collision);
    }

    private void OnCollisionStay(Collision collision)
    {
        ChanceToHit(collision);
    }

    private void ChanceToHit(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && !attacking)
        {
            int chance = rnd.Next(0, 100);
            if (chance < attackRate)
            {
                StartCoroutine("AttackPlayer");
            }
        }
    }

    IEnumerator AttackPlayer()
    {
        if (!isDead) 
        {       
            attacking = true;
            foreach (Animator anim in swords)
            {
                anim.SetTrigger("Attack");
                anim.SetTrigger("NoAttack");
                transform.GetComponent<AudioSource>().PlayOneShot(enemyAtk);
            }
            yield return new WaitForSeconds(dodgeTime);
            if (distanceFromPlayer <= 2)
            {
                player.GetComponent<PlayerStats>().ReduceHealth(attackPower);
            }
            yield return new WaitForSeconds(attackSpeed);
            attacking = false;
        } 
    }

    IEnumerator DelayedStart() 
    {
        // Ensure to Synch with Enemy Stats
        yield return new WaitForSeconds(0.4f);
        attackPower = gameObject.GetComponent<EnemyStats>().attackPower;
    }
}
