using System;
using System.Collections;
using System.Runtime.Serialization;
using UI.Windows;
using UnityEngine;
using UnityEngine.Events;

public class LvlManager_Gorfe : LvlManager
{
    public LvlHolder_Gorge _gorgeHolder;

    void Start()
    {
        _gorgeHolder = (LvlHolder_Gorge)_holder;

        _gorgeHolder.fadeImage.canvasRenderer.SetAlpha(0f);


        _gorgeHolder.Player.GetComponent<CharStats>().DeathEvent += PLAYERSDOX;

        FadeOut(0);
    }
    private void OnCheckpointSave(Chekpoint chek)
    {
        _gorgeHolder.myChekpoint = chek;
    }

    public void RelivePlayer()
    {

        _gorgeHolder.Player.GetComponent<new_PlayerMovement>().TeleportTo(_gorgeHolder.myChekpoint.transform.position);

    }


    bool chek1 = false;
    bool chek2 = false;

    private void Update()
    {
        if (chek1 && chek2)
        {
            OpenDoor();
        }
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
        Vector3 startPos = _gorgeHolder.mainDoor.position;
        Vector3 endPos = new Vector3(-42.75f, 70.8f, -29.81f);
        float elapsedTime = 0;
        while (elapsedTime < 1.0f)
        {
            _gorgeHolder.mainDoor.position = Vector3.Lerp(startPos, endPos, elapsedTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        _gorgeHolder.mainDoor.position = endPos;
    }


    public void BoxPlaced1()
    {
        chek1 = true;
    }public void BoxPlaced2()
    {
        chek2 = true;
    }

    public void EndLevel()
    {
        FadeTo(2);
        Invoke("EndLevelAction", 2f);

    }

    public void FadeTo(float time)
    {
        _gorgeHolder.fadeImage.CrossFadeAlpha(1f, time, false);
    }
    public void FadeOut(float time)
    {
        _gorgeHolder.fadeImage.CrossFadeAlpha(0f, time, false);
    }

    void EndLevelAction()
    {
        LoadLvl.StartLoad(E_Scenes.MainMenu);
    }

    public void PLAYERSDOX()
    {
        FadeTo(0.3f);
        _gorgeHolder.menu.SetActive(true);
    }
    public void toLoad()
    {
        _gorgeHolder.menu.SetActive(false);
        RelivePlayer();
        _gorgeHolder.Player.GetComponent<CharStats>().takeHeal(1000);
        FadeOut(0.5f);
    }
    public void ToMenu()
    {
        _gorgeHolder.menu.SetActive(false);
        LoadLvl.StartLoad(E_Scenes.MainMenu);
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

