using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class endGame : MonoBehaviour
{
    public LvlManager_Gorfe lvl;
    private void OnTriggerEnter(Collider other)
    {
        lvl.EndLevel();
    }
}
