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
        change = Vector3.zero;
        change.x = Input.GetAxis("Horizontal");
        change.z = Input.GetAxis("Vertical");

        //run towards player
        if (distance < MobDistanceRun)
        {
            Vector3 dirToPlayer = transform.position - Player.transform.position;
            Vector3 newPos = transform.position - dirToPlayer;
            Mob.SetDestination(newPos);
            animator.SetFloat("MoveX", change.x);
            animator.SetFloat("MoveZ", change.z);
            animator.SetBool("Moving", true);
        }
        else
        {
            animator.SetBool("Moving", false);
        }
    }
}