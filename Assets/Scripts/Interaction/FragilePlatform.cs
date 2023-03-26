using System.Collections;
using UnityEngine;

public class FragilePlatform : MonoBehaviour
{
    public float disappearTime = 2.0f;
    public float respawnTime = 5.0f;

    private bool isActive = true;
    private Coroutine disappearCoroutine;
    private Coroutine respawnCoroutine;
    private BoxCollider boxCollider;
    private MeshRenderer meshRenderer;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (disappearCoroutine == null && isActive)
            {
                disappearCoroutine = StartCoroutine(Disappear());
            }
        }
    }

    private IEnumerator Disappear()
    {
        yield return new WaitForSeconds(disappearTime);
        isActive = false;
        boxCollider.enabled = false;
        meshRenderer.enabled = false;
        disappearCoroutine = null;
        respawnCoroutine = StartCoroutine(Respawn());
    }

    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(respawnTime);
        isActive = true;
        boxCollider.enabled = true;
        meshRenderer.enabled = true;
        respawnCoroutine = null;
    }
}
