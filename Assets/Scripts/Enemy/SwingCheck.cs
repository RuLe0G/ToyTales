using UnityEngine;

public class SwingCheck : MonoBehaviour
{
    public int damage;

    public float knockBackForce;

    public Vector3 knockBackDirection;

    private LayerMask lmask;

    [Header("Audio")]
    private AudioSource aud;
    public AudioClip atcAud;
    public AudioClip hitAud;

    public Collider col;

    private void Start()
    {
        col = GetComponent<Collider>();

        aud = GetComponent<AudioSource>();
    }
    public void Activate(float t)
    {
        col.enabled = true;
        aud.clip = atcAud;
        aud.Play();
        Invoke(nameof(Deactivate), t);
    }
    public void Deactivate()
    {
        col.enabled = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        CheckCollision(other);
    }
    private void OnCollisionEnter(Collision collision)
    {

        CheckCollision(collision.collider);
    }

    private void CheckCollision(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            aud.clip = hitAud;
            aud.Play();
            Debug.Log("CheckCollision - Player");
        }
        if (other.gameObject.layer == lmask)
        {
            Debug.Log("CheckCollision - " + lmask.value);
        }
    }
}
