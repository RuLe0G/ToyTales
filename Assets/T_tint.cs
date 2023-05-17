using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T_tint : MonoBehaviour
{

    public Freezer freezer;
    private void Awake()
    {
        freezer = FindObjectOfType<Freezer>();
    }
    public void OK()
    {
        freezer.UnFreeze();
        Destroy(this.gameObject);
    }
}
