using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class LvlManager_Train : LvlManager
{
    private LvlHolder_Train _trainHolder;

    void Start()
    {
        _trainHolder = (LvlHolder_Train)_holder;
    }
    public void OpenDoor()
    {
        StartCoroutine(DoorMove());
    }
    IEnumerator DoorMove()
    {
        float a = 0;
        while (a < 1f)
        {
            a += Time.deltaTime;
            yield return null;
        }
        Vector3 startPos = _trainHolder.FirstDoor.position;
        Vector3 endPos = startPos + new Vector3(0, 0, 8);
        float elapsedTime = 0;
        while (elapsedTime < 1.0f)
        {
            _trainHolder.FirstDoor.position = Vector3.Lerp(startPos, endPos, elapsedTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        _trainHolder.FirstDoor.position = endPos;
    }
    public void ReturnPlayerCamera(float t)
    {
        StartCoroutine(ReturnCameras(t));
    }
    IEnumerator ReturnCameras(float t)
    {       
        float elapsedTime = 0;
        while (elapsedTime < t)
        {            
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        _trainHolder.DoorCamera.SetActive(false);
    }
    // Build Actions
    public void ActivateInitEvent()
    {
        if (_action.Length > 0)
        {
            UnityEvent initEvent = _action[0] as UnityEvent;
            initEvent?.Invoke();
        }
    }
    public void ActivateEndLvlEvent()
    {
        if (_action.Length > 0)
        {
            UnityEvent endLvlEvent = _action[1] as UnityEvent;
            endLvlEvent?.Invoke();
        }
    }
    public void ActivateRespawnCheckpoint()
    {
        if (_action.Length > 0)
        {
            UnityEvent respawnCheckpoint = _action[2] as UnityEvent;
            respawnCheckpoint?.Invoke();
        }
    }

    //Actions Invoke
    public void Activate0()
    {
        ActivateInitEvent();
    }
}
