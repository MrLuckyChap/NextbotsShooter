using UnityEngine;

namespace CodeBase.Weapon
{
  public class Shooting : MonoBehaviour
  {
    [SerializeField] private BulletManager _bulletManager;
    [SerializeField] private float _bulletSpeedCoefficient;

    private bool _isShootingAllowed;

    private void Update()
    {
      if (_isShootingAllowed) CheckInputTouch();
    }

    public void DisallowShooting() => _isShootingAllowed = false;
    public void AllowShooting() => _isShootingAllowed = true;

    private void CheckInputTouch()
    {
      if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
      {
        Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1000)) _bulletManager.Shoot(hit.point, _bulletSpeedCoefficient);
      }
    }
  }
}