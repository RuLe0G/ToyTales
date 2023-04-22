using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject decal;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(this.gameObject);
            return;
        }
        if(other.gameObject.layer == LayerMask.NameToLayer("ground"))
        {
            GameObject bh =  Instantiate(decal, this.transform.position, Quaternion.identity);
            bh.transform.Rotate(new Vector3(90,0,0));

            Destroy(bh, 10f);
        }
        Destroy(this.gameObject, 10f);
    }
}
