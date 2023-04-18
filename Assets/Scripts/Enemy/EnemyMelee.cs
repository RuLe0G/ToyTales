using UnityEngine;
using UnityEngine.AI;

public class EnemyMelee : MonoBehaviour
{
    private AudioSource aud;
    public float coolDown;
    public Enemy enm;
    private NavMeshAgent nma;
    private Animator anim;
    public LayerMask lmask;

    private float defaultCoolDown = 0.5f;

    private Rigidbody rb;
    private SwingCheck swingCheck;

    public Material originalMaterial;
    public Material biteMaterial;

    public bool damaging;

    public bool track;

    private void Start()
    {
        enm = GetComponent<Enemy>();
        nma = enm.nma;
        anim = enm.anim;


        TrackTick();
    }

    private void Update()
    {
        if (track && enm.target != null)
        {
            transform.LookAt(new Vector3(enm.target.position.x, transform.position.y, enm.target.position.z));

            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(new Vector3(enm.target.position.x, transform.position.y, enm.target.position.z) - transform.position), Time.deltaTime * 720f);
        }
        if (coolDown == 0f)
        {
            if (enm.target != null && Vector3.Distance(enm.target.position, transform.position) < 3f)
            {
                Swing();
            }
            return;
        }
        if (coolDown - Time.deltaTime > 0f)
        {
            coolDown -= Time.deltaTime / 2.5f;
            return;
        }
        coolDown = 0f;
    }
    private void OnEnable()
    {
        if (enm == null)
        {
            enm = GetComponent<Enemy>();
        }
        CancelAttack();
        if (rb)
        {
            rb.velocity = Vector3.zero;
            rb.isKinematic = true;
        }
    }
    private void FixedUpdate()
    {
        if (anim == null)
        {
            anim = enm.anim;
        }
        if (nma == null)
        {
            nma = enm.nma;
        }
        if (!(nma != null) || !nma.enabled || !nma.isOnNavMesh || !(enm.target != null))
        {
            if (nma == null)
            {
                nma = enm.nma;
            }
            return;
        }
        if (nma.isStopped || nma.velocity == Vector3.zero)
        {
            anim.SetBool("Running", false);
            return;
        }
        anim.SetBool("Running", true);
    }
    public void Swing()
    {
        if (aud == null)
        {
            aud = GetComponentInChildren<SwingCheck>().GetComponent<AudioSource>();
        }
        if (nma == null)
        {
            nma = enm.nma;
        }
        enm.stopped = true;
        anim.speed = 1f;
        track = true;
        coolDown = defaultCoolDown;
        if (nma.enabled)
        {
            nma.isStopped = true;
        }
        anim.SetTrigger("Swing");
    }
    public void SwingEnd()
    {
        if (nma.isOnNavMesh)
        {
            nma.isStopped = false;
        }
        enm.stopped = false;
    }
    public void DamageStart()
    {
        damaging = true;
        aud.Play();

        if (swingCheck == null)
        {
            swingCheck = base.GetComponentInChildren<SwingCheck>();
        }
        swingCheck.damage = 30;
        swingCheck.DamageStart();
    }
    public void DamageEnd()
    {
        if (rb == null)
        {
            rb = base.GetComponent<Rigidbody>();
        }
        damaging = false;
        enm.attacking = false;
        rb.velocity = Vector3.zero;
        rb.isKinematic = true;
        if (swingCheck == null)
        {
            swingCheck = base.GetComponentInChildren<SwingCheck>();
        }
        swingCheck.DamageStop();
    }
    public void StopTracking()
    {
        track = false;
        enm.attacking = true;
    }
    public void CancelAttack()
    {
        damaging = false;
        enm.attacking = false;
        enm.stopped = false;
        track = false;
        coolDown = defaultCoolDown;
        if (swingCheck == null)
        {
            swingCheck = base.GetComponentInChildren<SwingCheck>();
        }
        swingCheck.DamageStop();
    }

    public void TrackTick()
    {
        if (gameObject.activeInHierarchy)
        {
            if (nma == null)
            {
                nma = enm.nma;
            }
            if (nma != null && nma.enabled && nma.isOnNavMesh && enm.target != null)
            {
                RaycastHit raycastHit;
                if (Physics.Raycast(enm.target.position + Vector3.up * 0.1f, Vector3.down, out raycastHit, float.PositiveInfinity, lmask))
                {
                    nma.SetDestination(raycastHit.point);
                }
                else
                {
                    nma.SetDestination(enm.target.position);
                }
            }
        }
        Invoke("TrackTick", 0.1f);
    }
}
