using System;
using UnityEngine;
using UnityEngine.Events;

public class EnterTriggerComponent : MonoBehaviour
{
    [SerializeField] private string _tag;
    [SerializeField] private LayerMask _layer = ~0;
    [SerializeField] private EnterEvent _action;

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.gameObject.IsInLayer(_layer)) return;
        if (!string.IsNullOrEmpty(_tag) && !collision.gameObject.CompareTag(_tag)) return;

        _action?.Invoke(collision.gameObject);
    }
    [Serializable]
    public class EnterEvent : UnityEvent<GameObject>
    {

    }
}

public static class GameObjectExtensions
{
    public static bool IsInLayer(this GameObject go, LayerMask layer)
    {
        return layer == (layer | 1 << go.layer);
    }
}