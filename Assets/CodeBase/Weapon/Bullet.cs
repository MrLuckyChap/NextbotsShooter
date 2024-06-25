using System;
using UnityEngine;

namespace CodeBase.Weapon
{
  public class Bullet : MonoBehaviour
  {
    public event Action<Bullet> BulletHit;
    
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private float _bulletDamage;
    [SerializeField] private TriggerObserver _triggerObserver;
    [SerializeField] private LayerMask _consumptionMask;

    private float _bulletSpeedCoefficient;

    private void FixedUpdate()
    {
      transform.position += transform.forward * _bulletSpeed * _bulletSpeedCoefficient * Time.deltaTime;
    }

    private void Start()
    {
      _triggerObserver.TriggerEnter += TriggerEnter;
      _triggerObserver.TriggerExit += TriggerExit;
    }

    public void Initialize(float bulletSpeedCoefficient) => _bulletSpeedCoefficient = bulletSpeedCoefficient;

    private void TriggerEnter(Collider obj)
    {
      if ((_consumptionMask.value & (1 << obj.gameObject.layer)) != 0)
        obj.gameObject.GetComponent<IHealth>().TakeDamage(_bulletDamage);

      BulletHit?.Invoke(this);
    }

    private void TriggerExit(Collider obj)
    {
    }
  }
}