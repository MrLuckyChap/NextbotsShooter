using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using All_Imported_Assets.AMFPC.Enemy.Scripts;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.PoolObject;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
  public class GameFactory : IGameFactory
  {
    public GameObject PlayerGameObject { get; private set; }

    public MonoBehaviourPool<EnemyController> EnemyPool { get; private set; }

    private readonly IAssetProvider _assets;

    public GameFactory(IAssetProvider assets)
    {
      _assets = assets;
    }

    public async Task<GameObject> CreatePlayer(Vector3 at) => PlayerGameObject = _assets.Instantiate(AssetPath.PlayerPath, at);

    public async Task<GameObject> CreateHud() => _assets.Instantiate(AssetPath.HudPath);

    public async Task<GameObject> CreateGameObject(GameObject prefab) => Object.Instantiate(prefab);

    public async Task<GameObject> CreateGameObject(GameObject prefab, Vector3 position, Quaternion rotation) => Object.Instantiate(prefab, position, rotation);

    public async Task<List<GameObject>> CreateGameObjects(GameObject prefab, Vector3 position, Quaternion rotation, int count) =>
      Enumerable.Range(0, count)
        .Select(_ => Object.Instantiate(prefab, position, rotation))
        .ToList();

    public MonoBehaviourPool<EnemyController> CreateEnemy2DPool(EnemyController enemyPrefab, Vector3 position, int poolCount) =>
      new(enemyPrefab, position, poolCount);
  }
}