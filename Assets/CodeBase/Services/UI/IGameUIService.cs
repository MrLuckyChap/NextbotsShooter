using All_Imported_Assets.AMFPC.Camera.Scripts;
using All_Imported_Assets.AMFPC.Enemy.Scripts;
using CodeBase.Infrastructure.PoolObject;
using CodeBase.Infrastructure.StaticData;
using UnityEngine;

namespace CodeBase.Services.UI
{
  public interface IGameUIService
  {
    void Init(AISpawner aiSpawner, DevModeSpawnPosition devModeSpawnPosition, Transform playerTransform,
      MonoBehaviourPool<EnemyController> enemyPool);
    void SpawnUIClickedObject(EntityData entityData);
  }
}