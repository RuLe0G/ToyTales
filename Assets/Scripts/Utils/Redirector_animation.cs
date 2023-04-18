using UnityEngine;

public class Redirector_animation : MonoBehaviour
{
    [SerializeField] private Invoker _otherInvoke;


    private void Awake()
    {
        _otherInvoke = transform.GetComponent<Invoker>();
    }

    public void EventInvoke()
    {
        _otherInvoke.Interact();
    }
}
