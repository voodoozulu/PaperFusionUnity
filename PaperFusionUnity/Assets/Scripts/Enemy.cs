using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private NavMeshAgent Mob;
    public GameObject Player;
    public float MobDistanceRun = 4.0f;
    private Animator animator;
    private Vector3 change;
    [SerializeField] public List<Battler> enemiesToSpawn;
    public MapController map;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        Mob = GetComponent<NavMeshAgent>();   
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, Player.transform.position);

        //run towards player
        if (distance < MobDistanceRun)
        {
            Vector3 dirToPlayer = Player.transform.position - transform.position;
            Vector3 newPos = transform.position + dirToPlayer;
            Mob.SetDestination(newPos);
            animator.SetFloat("MoveX", dirToPlayer.x);
            animator.SetFloat("MoveZ", dirToPlayer.z);
            animator.SetBool("Moving", true);
        }
        else
        {
            animator.SetBool("Moving", false);
            Mob.SetDestination(transform.position);
        }
        Mob.transform.rotation = Quaternion.Euler(0, 0, 0);
    }


    void OnCollisionEnter(Collision collision)
    {
        map.BattleStart(enemiesToSpawn, "Normal Battle");
    }
}