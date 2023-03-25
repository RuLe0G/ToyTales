using System;
using UnityEngine;
using UnityEngine.Events;

public class ExitTriggerComponent : MonoBehaviour
{
    [SerializeField] private string _tag;
    [SerializeField] private LayerMask _layer = ~0;
    [SerializeField] private ExitEvent _action;

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.gameObject.IsInLayer(_layer)) return;
        if (!string.IsNullOrEmpty(_tag) && !collision.gameObject.CompareTag(_tag)) return;

        _action?.Invoke(collision.gameObject);
    }
    [Serializable]
    public class ExitEvent : UnityEvent<GameObject>
    {

    }
}


