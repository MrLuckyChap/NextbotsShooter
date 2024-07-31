using All_Imported_Assets.AMFPC.Camera.Scripts;
using All_Imported_Assets.AMFPC.Enemy.Scripts;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.PoolObject;
using CodeBase.Infrastructure.StaticData;
using UnityEngine;

namespace CodeBase.Services.UI
{
  public class GameGameUIService : IGameUIService
  {
    private readonly IGameFactory _gameFactory;
    private AISpawner _aiSpawner;
    private DevModeSpawnPosition _devModeSpawnPosition;
    private Transform _playerTransform;
    private MonoBehaviourPool<EnemyController> _enemyPool;

    public GameGameUIService(IGameFactory gameFactory)
    {
      _gameFactory = gameFactory;
    }

    public void Init(AISpawner aiSpawner, DevModeSpawnPosition devModeSpawnPosition, Transform playerTransform,
      MonoBehaviourPool<EnemyController> enemyPool)
    {
      _aiSpawner = aiSpawner;
      _devModeSpawnPosition = devModeSpawnPosition;
      _playerTransform = playerTransform;
      _enemyPool = enemyPool;
    }

    public void SpawnUIClickedObject(EntityData entityData)
    {
      if (entityData.Type == SpawnType.Enemy)
      {
        EnemyController enemyController = _enemyPool.Take();
        enemyController.SetValuesFromUISpawn(entityData.Special.ColliderSize, _enemyPool, _aiSpawner, _devModeSpawnPosition, _playerTransform);
      }
      else
      {
        GameObject newGameObject = _gameFactory
          .CreateGameObject(entityData.EntityPrefab, _devModeSpawnPosition.GetDevModeSpawnPoint(), Quaternion.identity).Result;
      }
    }
  }
}