using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable] class InitEvent : UnityEvent { }
[Serializable] class EndLvlEvent  : UnityEvent { }
[Serializable] class RespawnCheckpoint : UnityEvent { }

[RequireComponent(typeof(LvlHolder))]
public class LvlManager : MonoBehaviour
{
    [SerializeField]
    protected LvlHolder _holder;

    [SerializeField]
    protected UnityEvent[] _action;
    

    [SerializeField] InitEvent initEvent = null;
    [SerializeField] EndLvlEvent endLvlEvent = null;
    [SerializeField] RespawnCheckpoint respawnCheckpoint = null;

    private void Start()
    {
        _action= new UnityEvent[3] { initEvent, endLvlEvent, respawnCheckpoint };
    }

}
