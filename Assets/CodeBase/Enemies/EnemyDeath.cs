using System.Collections;
using CodeBase.Services.Enemy;
using UnityEngine;
using Zenject;

namespace CodeBase.Enemies
{
  [RequireComponent(typeof(EnemyHealth))]
  public class EnemyDeath : MonoBehaviour
  {
    [SerializeField] private EnemyHealth _health;
    [SerializeField] private GameObject _enemyGameObject;

    private IEnemyDeathService _enemyDeathService;

    [Inject]
    private void Constructor(IEnemyDeathService enemyDeathService)
    {
      _enemyDeathService = enemyDeathService;
    }

    private void Start()
    {
      _health.HealthChanged += OnHealthChanged;
    }

    private void OnHealthChanged()
    {
      if (_health.Current <= 0) Die();
    }

    private void Die()
    {
      _health.HealthChanged -= OnHealthChanged;
      _enemyDeathService.OneEnemyDied();
      StartCoroutine(DestroyTimer());
    }

    private IEnumerator DestroyTimer()
    {
      yield return new WaitForSeconds(0.1f);
      Destroy(_enemyGameObject);
    }

    private void OnDestroy()
    {
      _health.HealthChanged -= OnHealthChanged;
    }
  }
}