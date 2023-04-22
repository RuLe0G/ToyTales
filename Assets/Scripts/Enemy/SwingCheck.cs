using UnityEngine;

public class SwingCheck : MonoBehaviour
{
    public int damage;

    public float knockBackForce;

    public Vector3 knockBackDirection;

    private LayerMask lmask;

    private AudioSource aud;

    [HideInInspector]
    public Collider col;

    private bool physicalCollider;

    private bool collising;

    private void Start()
    {
        col = GetComponent<Collider>();
        if (!col.isTrigger)
        {
            physicalCollider = true;
        }
        else
        {
            DamageStart();
        }
        aud = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (collising)
        {
            CheckCollision(other);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collising)
        {
            CheckCollision(collision.collider);
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
        if (!physicalCollider)
        {
            col.enabled = true;
        }
    }
    public void DamageStop()
    {
        if (!physicalCollider)
        {
            if (col)
            {
                col.enabled = false;
            }
        }
    }
}
