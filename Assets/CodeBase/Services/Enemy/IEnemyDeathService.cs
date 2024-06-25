using System;

namespace CodeBase.Services.Enemy
{
  public interface IEnemyDeathService
  {
    event Action EnemyDied;

    void OneEnemyDied();
  }
}