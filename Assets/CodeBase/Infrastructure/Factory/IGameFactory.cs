using System.Collections.Generic;
using System.Threading.Tasks;
using All_Imported_Assets.AMFPC.Enemy.Scripts;
using CodeBase.Infrastructure.PoolObject;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
  public interface IGameFactory
  {
    Task<GameObject> CreatePlayer(Vector3 at);
    Task<GameObject> CreateHud();
    Task<GameObject> CreateGameObject(GameObject prefab);
    Task<GameObject> CreateGameObject(GameObject prefab, Vector3 position, Quaternion rotation);
    Task<List<GameObject>> CreateGameObjects(GameObject prefab, Vector3 position, Quaternion rotation, int count);
    GameObject PlayerGameObject { get; }
    MonoBehaviourPool<EnemyController> CreateEnemy2DPool(EnemyController enemyPrefab, Vector3 position, int poolCount);
  }
}