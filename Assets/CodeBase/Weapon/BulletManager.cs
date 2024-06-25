using CodeBase.Infrastructure.PoolObject;
using UnityEngine;

namespace CodeBase.Weapon
{
  public class BulletManager : MonoBehaviour
  {
    [SerializeField] private Bullet _bullet;
    [SerializeField] private Transform _spawn;
    [SerializeField] private int _poolSize;

    private MonoBehaviourPool<Bullet> _bulletPool;

    private void Start()
    {
      _bulletPool = new MonoBehaviourPool<Bullet>(_bullet, new GameObject("Bullets").transform, _poolSize);
    }

    private void OnBulletHit(Bullet bullet)
    {
      _bulletPool.Release(bullet);
      bullet.BulletHit -= OnBulletHit;
    }

    public void Shoot(Vector3 target, float bulletSpeedCoefficient)
    {
      Bullet bullet = _bulletPool.Take();
      bullet.BulletHit += OnBulletHit;
      bullet.transform.position = _spawn.position;
      bullet.transform.LookAt(target);
      bullet.Initialize(bulletSpeedCoefficient);
    }
  }
}