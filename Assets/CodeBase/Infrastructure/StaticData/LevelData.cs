using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace CodeBase.Infrastructure.StaticData
{
  [CreateAssetMenu(fileName = "LevelData", menuName = "Data/Level")]
  public class LevelData : ScriptableObject
  {
    public int LevelNumber;
    // public Vector3 InitialHeroPosition;
    // public List<Vector3> PlayerStopPositions;
    // public List<EnemySpawnPositions> EnemySpawnPositions;
    public GameObject Location;
    public NavMeshData NavMeshData;
    // public NavMeshData NavMeshData;
    public int EnemyCount;
    public int AlliesCount;
    public Vector3 PlayerCameraPosition;
    public List<GameObject> Enemy;
  }
}