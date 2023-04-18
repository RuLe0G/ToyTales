using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Redirector_animation : MonoBehaviour
{
    [SerializeField] private CactusMelee _scriptOnOtherObject;

    private void Awake()
    {
        _scriptOnOtherObject = transform.parent.GetComponent<CactusMelee>();
    }

    public void EventInvoke()
    {
        _scriptOnOtherObject.SwingEnd();
    }
}
