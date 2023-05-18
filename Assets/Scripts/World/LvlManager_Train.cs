using System;
using System.Collections;
using System.Runtime.Serialization;
using UI.Windows;
using UnityEngine;
using UnityEngine.Events;

public class LvlManager_Train : LvlManager
{
    public LvlHolder_Train _trainHolder;

    void Start()
    {
        _trainHolder = (LvlHolder_Train)_holder;

        _trainHolder.fadeImage.canvasRenderer.SetAlpha(0f);

        foreach (var enemy in _trainHolder.enemies)
        {
            enemy.DeathEvent += OnEnemyDeath;
        }

        foreach (var enemy in _trainHolder.enemies2)
        {
            enemy.DeathEvent += OnEnemy2Death;
        }

        foreach (var chekpoint in _trainHolder.chekpoints)
        {
            chekpoint.newSave += OnCheckpointSave;
        }

        _trainHolder.boss.DeathEvent += OnBossDeath;

        _trainHolder.Player.GetComponent<CharStats>().DeathEvent += PLAYERSDOX;

        FadeOut(0);
    }

    private void OnCheckpointSave(Chekpoint chek)
    {
        _trainHolder.myChekpoint = chek;
    }

    public void RelivePlayer() {

        _trainHolder.Player.GetComponent<new_PlayerMovement>().TeleportTo(_trainHolder.myChekpoint.transform.position);
    
    }
    void OnBossDeath(Enemy enemy)
    {
        chek3 = true;
    }
    void OnEnemyDeath(Enemy enemy)
    {
        _trainHolder.enemies.Remove(enemy);
    }
    void OnEnemy2Death(Enemy enemy)
    {
        _trainHolder.enemies2.Remove(enemy);
    }

    bool chek1 = true;
    bool chek2 = true;
    bool chek3 = false;

    private void Update()
    {
        if (chek1 && _trainHolder.enemies.Count <= 0) 
        {
            chek1 = false;

            Activate0();
        }
        if (chek2 && _trainHolder.enemies2.Count <= 0)
        {
            if(_trainHolder.enemies2.Count >= 2)
            { }
            else
            SpawnEnemy();
        }
        if (chek3)
        {
            OpenDoor2();
        }
    }

    public GameObject enmPref;

    public void SpawnEnemy()
    {
        Destroy(Instantiate(_trainHolder.Pof, new Vector3(-2.72f, 5.03f, -11.05f), Quaternion.identity), 2f);
        GameObject enm = Instantiate(enmPref, new Vector3(-2.72f, 5.03f, -11.05f), Quaternion.identity);
        _trainHolder.enemies2.Add(enm.GetComponent<Enemy>());
        enm.GetComponent<Cactus>().DeathEvent += OnEnemy2Death;

        Destroy(Instantiate(_trainHolder.Pof, new Vector3(5.13f, 5.03f, -11.05f), Quaternion.identity), 2f);
        GameObject enm2 = Instantiate(enmPref, new Vector3(5.13f, 5.03f, -11.05f), Quaternion.identity);
        _trainHolder.enemies2.Add(enm2.GetComponent<Enemy>());
        enm2.GetComponent<Cactus>().DeathEvent += OnEnemy2Death;
    }
    public void BoxPlaced()
    {
        chek2 = false;
    }

    public void EndLevel()
    {
        FadeTo(2);
        Invoke("EndLevelAction", 2f);

    }

    public void FadeTo(float time)
    {
        _trainHolder.fadeImage.CrossFadeAlpha(1f, time, false);
    }public void FadeOut(float time)
    {
        _trainHolder.fadeImage.CrossFadeAlpha(0f, time, false);
    }

    void EndLevelAction()
    {
        LoadLvl.StartLoad(E_Scenes.Level2Gorge);
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
    public void OpenDoor2()
    {
        StartCoroutine(DoorMove2());
    }
    IEnumerator DoorMove2()
    {
        float a = 0;
        while (a < 1.5f)
        {
            a += Time.deltaTime;
            yield return null;
        }
        Vector3 startPos = _trainHolder.door2.position;
        Vector3 endPos = startPos - new Vector3(0, 10, 0);
        float elapsedTime = 0;
        while (elapsedTime < 1.0f)
        {
            _trainHolder.door2.position = Vector3.Lerp(startPos, endPos, elapsedTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        _trainHolder.door2.position = endPos;
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
    public void PLAYERSDOX()
    {
        FadeTo(0.3f);
        _trainHolder.menu.SetActive(true);
    }
    public void toLoad()
    {
        _trainHolder.menu.SetActive(false);
        RelivePlayer();
        _trainHolder.Player.GetComponent<CharStats>().takeHeal(1000);
        FadeOut(0.5f);
    }
    public void ToMenu()
    {
        _trainHolder.menu.SetActive(false);
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
