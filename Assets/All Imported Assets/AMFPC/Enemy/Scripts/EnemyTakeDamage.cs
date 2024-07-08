using All_Imported_Assets.AMFPC.Scripts;
using All_Imported_Assets.AMFPC.Scripts.Interfaces;
using UnityEngine;

namespace All_Imported_Assets.AMFPC.Enemy.Scripts
{
  public class EnemyTakeDamage : MonoBehaviour, IDamageable
  {
    [SerializeField] private HealthManager _healthManager;

    public void Damage(int value) => _healthManager.Damage(value);
  }
}