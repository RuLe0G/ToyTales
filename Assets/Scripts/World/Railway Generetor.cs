using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class RailwayGeneretor : MonoBehaviour
{
    public RoadData[] roadDatas;
    public RoadData firstRoad;

    private RoadData prevRoad;

    public Vector3 spawnOrigin;

    private Vector3 spawnPos;
    public int roadsToSpawn;

    public float roadSpeed;

    [Header("Debug")]
    private ThridPersonAsset playerActionsAsset;

    private void Awake()
    {
        playerActionsAsset = new ThridPersonAsset();
    }
    private void OnEnable()
    {

        playerActionsAsset.Player.DEBUG.started += DebugKey;
        playerActionsAsset.Player.Enable();
        TriggerExit.OnRoadExited += SpawnRoad;
    }
    private void OnDisable()
    {
        playerActionsAsset.Player.DEBUG.started -= DebugKey;
        playerActionsAsset.Player.Disable();
        TriggerExit.OnRoadExited -= SpawnRoad;
    }

    private void DebugKey(InputAction.CallbackContext obj)
    {
        SpawnRoad();
    }


    private void Start()
    {
        prevRoad = firstRoad;

        for (int i = 0; i < roadsToSpawn; i++)
        {
            SpawnRoad();
        }
    }

    RoadData TakeNextRoad()
    {
        List<RoadData> allowedRoadsList = new List<RoadData>();
        RoadData nextRoad = null;

        RoadData.Direction nextReqDir = RoadData.Direction.North;

        switch (prevRoad.exixDir)
        {
            case RoadData.Direction.North:
                nextReqDir = RoadData.Direction.South;
                spawnPos = spawnPos + new Vector3(0f, 0, prevRoad.roadSize.y);
                break;

            case RoadData.Direction.South:
                nextReqDir = RoadData.Direction.North;
                spawnPos = spawnPos + new Vector3(0f, 0, -prevRoad.roadSize.y);
                break;

            case RoadData.Direction.West:
                nextReqDir = RoadData.Direction.East;
                spawnPos = spawnPos + new Vector3(-prevRoad.roadSize.x, 0, 0f);
                break;

            case RoadData.Direction.East:
                nextReqDir = RoadData.Direction.West;
                spawnPos = spawnPos + new Vector3(prevRoad.roadSize.x, 0, 0f);
                break;

            default:
                break;
        }

        for (int i = 0; i < roadDatas.Length; i++)
        {
            if (roadDatas[i].entryDir == nextReqDir)
                allowedRoadsList.Add(roadDatas[i]);
        }
        nextRoad = allowedRoadsList[Random.Range(0, allowedRoadsList.Count)];
        return nextRoad;
    }

    private void FixedUpdate()
    {
        spawnPos += (Vector3.forward * roadSpeed * Time.fixedDeltaTime);
    }

    void SpawnRoad()
    {
        RoadData roadToSpawn = TakeNextRoad();

        GameObject obj = roadToSpawn.levelRoads[Random.Range(0, roadToSpawn.levelRoads.Length)];


        prevRoad = roadToSpawn;
        GameObject obj2 = Instantiate(obj, spawnPos + spawnOrigin, Quaternion.identity);
        obj2.GetComponent<RoadMovement>().SetSpeed(roadSpeed);
    }

    public void UpdateSpawnOrigin(Vector3 originDelta)
    {
        spawnOrigin = spawnOrigin + originDelta;
    }
}
