using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.ProBuilder;

public class Cactus : Enemy
{
    [Header("Attack")]
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public int maxAtkCount = 10;
    private int atkCount = 0;
    public bool isReload = false;
    public SwingCheck swingCheck;

    public float sightRange, attkRange;
    public bool playerInSightRange, playerInAttackRange;

    [Header("walkPoint")]
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    bool death = false;

    LayerMask whatIsGround;
    private void Awake()
    {
        whatIsGround = LayerMask.GetMask("ground");
        player = GameObject.Find("Player").transform;
        if (swingCheck == null)
        {
            swingCheck = GetComponentInChildren<SwingCheck>();
        }
    }
    private void Update()
    {
        if (!death)
        {
            playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
            playerInAttackRange = Physics.CheckSphere(transform.position, attkRange, whatIsPlayer);

            if (!playerInSightRange && !playerInAttackRange && !isReload) Stay();
            if (playerInSightRange && !playerInAttackRange && !isReload && canMove) ChasePlayer();
            if (playerInAttackRange && playerInSightRange && !isReload) AttackPlayer();
        }
    }
    private void Stay()
    {

    }
    private void ChasePlayer()
    {
        nma.SetDestination(player.position);
        anim.SetInteger("run", 1);
    }
    private void AttackPlayer()
    {
        nma.SetDestination(transform.position);
        anim.SetInteger("run", 0);


        if (!alreadyAttacked)
        {
            anim.SetTrigger("Attck");

            canMove = false;


            alreadyAttacked = true;

            atkCount++;
            if (atkCount >= maxAtkCount)
            {
                isReload = true;
                ResetShootCount();
            }

            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    bool canMove = true;
    public void InitAttk()
    {
        swingCheck.damage = 30;
        swingCheck.Activate(0.1f);
        
    }
    private void ResetShootCount()
    {
        atkCount = 0;
        isReload = false;
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
        canMove = true;
    }

    protected override void Death()
    {
        death = true;
        base.Death();
    }

    //DEBUG GIZMOS
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attkRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}