using System;
using All_Imported_Assets.AMFPC.Player_Controller.Scripts;
using CodeBase.Infrastructure.Factory;
using UnityEngine;
using UnityEngine.AI;

namespace All_Imported_Assets.AMFPC.Enemy.Scripts
{
  public class AgentMoveToPlayer : MonoBehaviour
  {
    public event Action PlayerCaught;

    [SerializeField] private NavMeshAgent _agent;
    // [SerializeField] private TriggerObserver _triggerObserver;

    private const float MinimalDistance = 0.5f;

    private Transform _playerTransform;
    private IGameFactory _gameFactory;

    public void Init(Transform playerTransform)
    {
      _playerTransform = playerTransform;
    }

    private void TriggerEnter(Collider obj)
    {
      //todo: enemy catch player with collider, not Vector3.SqrMagnitude
    }

    private void Update()
    {
      if (IsHeroNotReached()) _agent.destination = _playerTransform.position;
      else
      {
        _playerTransform.GetComponent<DamageManager>().TakeDamage(110);
        PlayerCaught?.Invoke();
      }
    }

    private bool IsHeroNotReached() =>
      Vector3.SqrMagnitude(_playerTransform.position - _agent.transform.position) >= MinimalDistance;
  }
}