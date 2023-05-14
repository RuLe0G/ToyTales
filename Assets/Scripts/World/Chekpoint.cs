using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chekpoint : MonoBehaviour
{
    public event Action<Chekpoint> newSave;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            newSave?.Invoke(this);
            Debug.Log("Save");
        }
    }
}
