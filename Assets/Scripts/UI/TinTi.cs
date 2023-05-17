using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class TinTi : MonoBehaviour
{
    public GameObject Tint;
    public Transform GlobalTint;
    public Freezer freezer;
    bool isSeen;

    private void Awake()
    {
        freezer = FindObjectOfType<Freezer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Instantiate(Tint, GlobalTint);
            freezer.FreezeNoTimer();
            isSeen = true;
            Destroy(this.gameObject);
        }
    }
}
