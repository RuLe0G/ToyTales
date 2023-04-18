using UnityEngine;
using UnityEngine.AI;

public class Cactus : MonoBehaviour
{
    [Header("Base")]
    public float health;
    private Rigidbody rb;
    public GameObject player;
    public NavMeshAgent nma;
    private float curSpeed;
    private float defaultSpeed;
    public Vector3 agentVelocity;

    [Header("Visual")]
    public Animator anim;
    public Material mat;
    public Renderer smr;

    [Header("Audio")]
    private AudioSource aud;
    public AudioClip[] hurtSounds;
    public AudioClip deathSound;

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

        defaultSpeed = nma.speed;
        nma.acceleration = 30f;
        nma.angularSpeed = 400f;
        nma.speed = 5f;
    }
    public void GetHurt()
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
        }
    }
    private void Death()
    {
        aud.clip = deathSound;
        aud.pitch = UnityEngine.Random.Range(0.85f, 1.35f);
        aud.Play();

        anim.SetTrigger("Death");

        target = null;

        rb.isKinematic = false;
        rb.useGravity = true;
        rb.angularDrag = 0.001f;
        rb.maxAngularVelocity = float.PositiveInfinity;
        rb.velocity = Vector3.zero;

        Vector3 normalized = (player.transform.position - transform.position).normalized;
        normalized = -normalized;
        rb.AddForce(normalized * 5f, ForceMode.Impulse);
        rb.AddTorque(rb.transform.right * 0.3f, ForceMode.VelocityChange);

        rb = null;
        nma = null;
    }
}
