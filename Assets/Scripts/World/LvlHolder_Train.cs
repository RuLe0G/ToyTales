using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LvlHolder_Train : LvlHolder
{
    [Header("GamePbjs")]
    public Transform FirstDoor;

    public List<Enemy> enemies = new List<Enemy>();

    public List<Enemy> enemies2 = new List<Enemy>();

    public GameObject Pof;

    public Image fadeImage;

    public List<Chekpoint> chekpoints = new List<Chekpoint>();

    public Chekpoint myChekpoint;

    [Header("Cameras")]
    public GameObject DoorCamera;
}
