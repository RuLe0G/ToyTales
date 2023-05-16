using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TP_Player : MonoBehaviour
{
    public LvlManager_Train manager;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            manager.FadeTo(0.5f);
            Invoke("Prik", 0.5f);
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            manager.FadeTo(0.5f);
            Invoke("Prik", 0.5f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            manager.FadeTo(0.5f);
            Invoke("Prik", 0.5f);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            manager.FadeTo(0.5f);
            Invoke("Prik", 0.5f);
        }
    }

    private void Prik()
    {
        manager.RelivePlayer();
        manager.FadeOut(0.5f);
    }
}
