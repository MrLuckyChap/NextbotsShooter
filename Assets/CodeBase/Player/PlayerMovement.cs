using System.Collections.Generic;
using CodeBase.Services.Level;
using CodeBase.Services.StaticData;
using CodeBase.Services.UI;
using CodeBase.Weapon;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace CodeBase.Player
{
  public class PlayerMovement : MonoBehaviour
  {
    [SerializeField] private NavMeshAgent _navMeshAgent;
    [SerializeField] private PlayerAnimator _animator;
    [SerializeField] private Shooting _shooting;
    [SerializeField] private EnemyKillCounter _enemyKillCounter;

    private IStaticDataService _dataService;
    private IUIService _uiService;
    private ILevelService _levelService;
    private List<Vector3> _playerStopPositions;
    private int _pointAchievedQuantity;
    private bool _isFirstCondition = true;

    [Inject]
    private void Constructor(IStaticDataService dataService, IUIService uiService, ILevelService levelService)
    {
      _dataService = dataService;
      _uiService = uiService;
      _levelService = levelService;
      _uiService.TapAreaClicked += OnTapAreaClicked;
      // _playerStopPositions = _dataService.ForLevel(1).PlayerStopPositions;
    }

    private void Awake()
    {
      _shooting.DisallowShooting();
      _enemyKillCounter.AllEnemiesOnPointKilled += OnAllEnemiesOnPointKilled;
    }

    private void OnTapAreaClicked() => MoveToPoint();

    private void FixedUpdate()
    {
      if (_pointAchievedQuantity < _playerStopPositions.Count && _navMeshAgent.remainingDistance < 0.3f &&
          _navMeshAgent.hasPath && _isFirstCondition)
      {
        _shooting.AllowShooting();
        _animator.ResetToIdle();
        _pointAchievedQuantity += 1;
        _isFirstCondition = false;
      }

      if (_pointAchievedQuantity == _playerStopPositions.Count && _navMeshAgent.remainingDistance < 0.3f &&
          _navMeshAgent.hasPath && !_isFirstCondition)
      {
        _isFirstCondition = true;
        _levelService.NotifyListenersAboutPassedLastPoint();
      }
    }

    private void MoveToPoint()
    {
      Vector3 targetPosition = _playerStopPositions[_pointAchievedQuantity];
      _navMeshAgent.SetDestination(targetPosition);
      _animator.PlayRunning();
      _isFirstCondition = true;
    }

    private void OnAllEnemiesOnPointKilled()
    {
      _shooting.DisallowShooting();
      MoveToPoint();
    }

    private void OnDestroy()
    {
      _uiService.TapAreaClicked -= OnTapAreaClicked;
    }
  }
}