using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Bullet : MonoBehaviour
{
    public GameObject decal;
    public GameObject particle;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameObject partic = Instantiate(particle);
            partic.transform.localScale = new Vector3(0.1f,0.1f,0.1f);
            partic.transform.position = this.transform.position;
            Destroy(partic, 0.5f);
            other.GetComponent<CharStats>().takeDamage(3);
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
