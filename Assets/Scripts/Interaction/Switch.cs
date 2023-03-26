using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Switch : MonoBehaviour
{
    [Header("Settings")]
    public bool isTimer;
    public float _timer;

    [SerializeField] private UnityEvent _onAction;
    [SerializeField] private UnityEvent _offAction;

    private bool isActive;

    public void Action()
    {
        if (!isTimer)
        {
            if (!isActive)
            {
                isActive = true;
                onAction();
            }
            else
            {
                isActive = false;
                offAction();
            }
        }
        else
        {
            if (!isActive)
            {
                isActive = true;
                onAction();
                StartCoroutine(OffActionCoroutine());
            }
        }
    }

    public void onAction()
    {
        _onAction?.Invoke();
    }

    public void offAction()
    {
        _offAction?.Invoke();
    }

    IEnumerator OffActionCoroutine()
    {
        yield return new WaitForSeconds(_timer);
        isActive = false;
        offAction();
    }
}
