using System.Collections.Generic;
using UnityEngine;

public class LvlHolder_Train : LvlHolder
{
    [Header("GamePbjs")]
    public Transform FirstDoor;

    public List<Enemy> enemies = new List<Enemy>();

    public List<Enemy> enemies2 = new List<Enemy>();

    public GameObject Pof;




    [Header("Cameras")]
    public GameObject DoorCamera;
}
