using System.Collections;
using UnityEngine;

public class Freezer : Singleton<Freezer>
{
    [Range(0f, 1f)]
    public float duration = 0.1f;

    bool _isFrozen = false;
    float _pendingFreezeDuration = 0f;

    private void Update()
    {
        if (_pendingFreezeDuration > 0 && !_isFrozen)
        {
            StartCoroutine(DoFreeze());
        }
    }


    public void Freeze()
    {
        _pendingFreezeDuration = duration;
    }

    IEnumerator DoFreeze()
    {
        _isFrozen = true;
        var original = Time.timeScale;
        Time.timeScale = 0f;

        yield return new WaitForSecondsRealtime(duration);

        Time.timeScale = original;
        _pendingFreezeDuration = 0;
        _isFrozen = false;
    }
}
