using UnityEngine;

public class Solider : Enemy
{
    [Header("Attack")]
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile;
    public int maxShootCount = 10;
    private int shootCount = 0;
    public bool isReload = false;

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
    }

    private void Update()
    {
        if(!death)
        { 
            playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
            playerInAttackRange = Physics.CheckSphere(transform.position, attkRange, whatIsPlayer);

            if (!playerInSightRange && !playerInAttackRange && !isReload) Stay();
            if (playerInSightRange && !playerInAttackRange && !isReload) ChasePlayer();
            if (playerInAttackRange && playerInSightRange && !isReload) AttackPlayer();
            if (isReload) Patroling();
        }
    }
    private void Stay()
    {

    }
    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            nma.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 1f)
        { 
            walkPointSet = false;
            ResetShootCount();
        }
    }
    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        randomZ = Mathf.Clamp(randomZ, 2, walkPointRange);
        randomX = Mathf.Clamp(randomZ, 2, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }
    private void ChasePlayer()
    {
        nma.SetDestination(player.position);
    }
    private void AttackPlayer()
    {
        nma.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            rb.AddForce(transform.up * 3f, ForceMode.Impulse);

            alreadyAttacked = true;

            shootCount++;
            if (shootCount >= maxShootCount)
                isReload = true;

            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetShootCount()
    {
        shootCount = 0;
        isReload = false;
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
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
