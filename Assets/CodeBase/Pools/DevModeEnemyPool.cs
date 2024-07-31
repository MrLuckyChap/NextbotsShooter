using All_Imported_Assets.AMFPC.Enemy.Scripts;
using CodeBase.Infrastructure.PoolObject;
using UnityEngine;
using Zenject;

namespace CodeBase.Pools
{
  public class DevModeEnemyPool : MonoBehaviour
  {
    private MonoBehaviourPool<EnemyController> _enemyPool;

    [Inject]
    public void Init()
    {
      
    }
  }
}