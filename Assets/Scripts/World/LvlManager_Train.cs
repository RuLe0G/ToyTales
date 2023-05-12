using System.Collections;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.Events;

public class LvlManager_Train : LvlManager
{
    private LvlHolder_Train _trainHolder;

    void Start()
    {
        _trainHolder = (LvlHolder_Train)_holder;

        foreach (var enemy in _trainHolder.enemies)
        {
            enemy.DeathEvent += OnEnemyDeath;
        }

        foreach (var enemy in _trainHolder.enemies2)
        {
            enemy.DeathEvent += OnEnemy2Death;
        }
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
