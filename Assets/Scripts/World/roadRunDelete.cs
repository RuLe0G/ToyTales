using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roadRunDelete : MonoBehaviour
{
    public float delay = 30f;

    private void Start()
    {
        StartCoroutine(WaitAndDeactivate());
    }
    IEnumerator WaitAndDeactivate()
    {
        yield return new WaitForSeconds(delay);

        Destroy(transform.root.gameObject);

    }
}
