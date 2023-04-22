using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerExit : MonoBehaviour
{
    public float delay = 5f;

    public delegate void ExitAction();
    public static event ExitAction OnRoadExited;

    private bool exited = false;

    private void OnTriggerExit(Collider other)
    {
        TrainTag trainTag = other.GetComponent<TrainTag>();
        if (trainTag != null)
        {
            if (!exited)
            {
                exited = true;
                OnRoadExited();
                StartCoroutine(WaitAndDeactivate());
            }
        }

        IEnumerator WaitAndDeactivate()
        {
            yield return new WaitForSeconds(delay);

            Destroy(transform.root.gameObject);

        }
    }
}
