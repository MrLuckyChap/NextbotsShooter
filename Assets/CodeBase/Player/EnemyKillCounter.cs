using System;
using System.Collections.Generic;
using CodeBase.Infrastructure.StaticData;
using CodeBase.Services.Enemy;
using CodeBase.Services.StaticData;
using UnityEngine;
using Zenject;

namespace CodeBase.Player
{
  public class EnemyKillCounter : MonoBehaviour
  {
    public event Action AllEnemiesOnPointKilled;
    
    private IStaticDataService _dataService;
    private IEnemyDeathService _enemyDeathService;
    private List<int> _enemyAmountOnPoints = new();
    private int _currentNumberPoint;
    private int _enemyAmountOnPoint;

    [Inject]
    private void Constructor(IStaticDataService dataService, IEnemyDeathService enemyDeathService)
    {
      _dataService = dataService;
      _enemyDeathService = enemyDeathService;
    }

    private void Start()
    {
      // foreach (EnemySpawnPositions positions in _dataService.ForLevel(1).EnemySpawnPositions)
      //   _enemyAmountOnPoints.Add(positions.Positions.Count);

      _enemyDeathService.EnemyDied += OnEnemyDied;
    }

    private void OnEnemyDied()
    {
      _enemyAmountOnPoints[_currentNumberPoint] -= 1;
      if (_enemyAmountOnPoints[_currentNumberPoint] <= 0)
      {
        AllEnemiesOnPointKilled?.Invoke();
        if (_enemyAmountOnPoints.Count > _currentNumberPoint) _currentNumberPoint += 1;
      }
    }

    private void OnDestroy()
    {
      _enemyDeathService.EnemyDied -= OnEnemyDied;
    }
  }
}