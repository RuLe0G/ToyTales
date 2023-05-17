using System.Collections;
using UnityEngine;
using static UnityEngine.UI.Image;

public class Freezer : Singleton<Freezer>
{
    [Range(0f, 1f)]
    public float duration = 0.1f;

    bool _isFrozen = false;
    float _pendingFreezeDuration = 0f;

    float original;
    private void Start()
    {
        original = Time.timeScale;
    }

    private void Update()
    {
        if (_pendingFreezeDuration > 0 && !_isFrozen)
        {
            StartCoroutine(DoFreeze());
        }
    }

    public void FreezeNoTimer()
    {
        _isFrozen = true;
        Time.timeScale = 0f;
    }
    public void UnFreeze()
    {
        Time.timeScale = original;
        _isFrozen = false;
    }

    public void Freeze()
    {
        _pendingFreezeDuration = duration;
    }

    IEnumerator DoFreeze()
    {
        _isFrozen = true;
        Time.timeScale = 0f;

        yield return new WaitForSecondsRealtime(duration);

        Time.timeScale = original;
        _pendingFreezeDuration = 0;
        _isFrozen = false;
    }
}
