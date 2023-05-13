using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menuManager2 : MonoBehaviour
{
    public GameObject[] cheliks;
    float delay;
    int i;

    void Start()
    {
        delay = Random.Range(5f, 7f);

        StartCoroutine(TimerCoroutine(delay));
    }
    IEnumerator TimerCoroutine(float d)
    {
        yield return new WaitForSeconds(d);

        Activate();
    }
    void Activate()
    {
        if (i > 2)
                i = 0;
        delay = Random.Range(5f, 7f);
        StartCoroutine(TimerCoroutine(delay));

        Destroy(Instantiate(cheliks[i]), 15f);
        i++;
    }
}
