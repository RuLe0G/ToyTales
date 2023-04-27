using UnityEngine;
using UnityEngine.AI;

public abstract class Enemy : MonoBehaviour
{
    [Header("Base")]
    public float health;
    private Rigidbody rb;
    public Transform player;
    public NavMeshAgent nma;

    [Header("Movement")]
    public float _speed = 10f;
    public float _angularSpeed = 400f;
    public float _acceleration = 30f;


    [Header("Visual")]
    public Animator anim;
    public Renderer smr;

    [Header("Audio")]
    private AudioSource aud;
    public AudioClip[] hurtSounds;
    public AudioClip deathSound;

    public LayerMask whatIsPlayer;
    public Transform target;

    public bool stopped;

    public bool attacking;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        smr = GetComponentInChildren<SkinnedMeshRenderer>();
        anim = GetComponentInChildren<Animator>();
        aud = GetComponent<AudioSource>();

        target = player.transform;

        nma.acceleration = _speed;
        nma.angularSpeed = _angularSpeed;
        nma.speed = _acceleration;
    }
    public virtual void GetHurt()
    {
        float num = 1;

        health -= num;

        if (health <= 0)
        {
            Death();
        }
        else
        {
            aud.clip = hurtSounds[UnityEngine.Random.Range(0, hurtSounds.Length)];
            aud.pitch = UnityEngine.Random.Range(0.85f, 1.35f);
            aud.Play();
            anim.SetTrigger("TakeDamage");
        }
    }

    protected virtual void Death()
    {
        this.aud.clip = this.deathSound;
        this.aud.pitch = UnityEngine.Random.Range(0.85f, 1.35f);
        this.aud.Play();

        anim.SetTrigger("Death");

        target = null;

        rb.isKinematic = false;
        rb.useGravity = true;
        rb.angularDrag = 0.001f;
        rb.maxAngularVelocity = float.PositiveInfinity;
        rb.velocity = Vector3.zero;

        //Vector3 normalized = (player.transform.position - transform.position).normalized;
        //normalized = -normalized;
        //rb.AddForce(normalized * 5f, ForceMode.Impulse);
        //rb.AddTorque(rb.transform.right * 0.3f, ForceMode.VelocityChange);

        rb = null;
        nma = null;
    }
}
