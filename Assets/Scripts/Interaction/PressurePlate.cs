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
        Collider[] colliders = Physics.OverlapBox(transform.position, transform.lossyScale / 2f, Quaternion.identity, objectsLayer);
        active = colliders.Length > 0;

        if (!active && _wasActive)
        {
            CancelAction();
        }

        if (active && !_wasActive)
        {
            Action();
        }

        _wasActive = active;
    }

}