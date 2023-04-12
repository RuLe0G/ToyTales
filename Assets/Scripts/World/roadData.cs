using UnityEngine;

[CreateAssetMenu(menuName = "LevelRoadData")]
public class RoadData : ScriptableObject
{
    public enum Direction
    {
        North,
        East,
        South,
        West
    }
    public Vector2 roadSize = new Vector2(10f, 10f);

    public GameObject[] levelRoads;
    public Direction entryDir;
    public Direction exixDir;
}
