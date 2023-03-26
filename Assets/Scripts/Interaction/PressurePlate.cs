using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;

public class PressurePlate : MonoBehaviour
{
    public LayerMask objectsLayer;
    public bool active;

    [SerializeField] private UnityEvent _action;
    [SerializeField] private UnityEvent _cancelAction;

    private bool _wasActive = false;

    public void Action()
    {
        _action?.Invoke();
    }

    public void CancelAction()
    {
        _cancelAction?.Invoke();
    }

    private void FixedUpdate()
    {
        Collider[] colliders = Physics.OverlapBox(transform.position, transform.localScale / 2f, Quaternion.identity, objectsLayer);
        active = colliders.Length > 0;

        // если плита стала неактивной и была активной ранее
        if (!active && _wasActive)
        {
            CancelAction();
        }

        // если плита стала активной и была неактивной ранее
        if (active && !_wasActive)
        {
            Action();
        }

        _wasActive = active;
    }

}