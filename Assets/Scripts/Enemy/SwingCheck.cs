using UnityEngine;

public class SwingCheck : MonoBehaviour
{
    public int damage;

    public int enemyDamage;

    public float knockBackForce;

    public Vector3 knockBackDirection;

    private LayerMask lmask;

    [HideInInspector]
    public Collider col;

    public bool useRaycastCheck;

    private AudioSource aud;

    private bool physicalCollider;

    [HideInInspector]
    public bool damaging;

    public bool startActive;

    public bool interpolateBetweenFrames;

    private Vector3 previousPosition;

    private bool ignoreTick;

    private void Start()
    {
        this.col = base.GetComponent<Collider>();
        if (!this.col.isTrigger)
        {
            this.physicalCollider = true;
        }
        else if (!this.startActive)
        {
            this.col.enabled = false;
        }
        else
        {
            this.DamageStart();
        }
        if (this.interpolateBetweenFrames)
        {
            this.previousPosition = base.transform.position;
        }
        this.aud = base.GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (this.damaging)
        {
            this.CheckCollision(other);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (this.damaging)
        {
            this.CheckCollision(collision.collider);
        }
    }
    private void Update()
    {
        if (this.interpolateBetweenFrames && this.damaging && this.col.attachedRigidbody)
        {
            if (!this.ignoreTick)
            {
                foreach (RaycastHit raycastHit in this.col.attachedRigidbody.SweepTestAll(this.previousPosition - base.transform.position, Vector3.Distance(this.previousPosition, base.transform.position), QueryTriggerInteraction.Collide))
                {
                    this.CheckCollision(raycastHit.collider);
                }
            }
            else
            {
                this.ignoreTick = false;
            }
            this.previousPosition = base.transform.position;
        }
    }
    private void CheckCollision(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("CheckCollision - Player");
        }
    }
    public void DamageStart()
    {
        this.ignoreTick = true;
        this.damaging = true;
        if (!this.physicalCollider)
        {
            this.col.enabled = true;
        }
        if (this.aud != null)
        {
            this.aud.Play();
        }
    }
    public void DamageStop()
    {
        this.damaging = false;
        if (!this.physicalCollider)
        {
            if (this.col)
            {
                this.col.enabled = false;
            }
        }
    }
}
