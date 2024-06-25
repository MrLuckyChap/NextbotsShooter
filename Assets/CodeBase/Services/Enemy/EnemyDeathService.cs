using System;

namespace CodeBase.Services.Enemy
{
  public class EnemyDeathService : IEnemyDeathService
  {
    public event Action EnemyDied;

    public void OneEnemyDied() => EnemyDied?.Invoke();
  }
}