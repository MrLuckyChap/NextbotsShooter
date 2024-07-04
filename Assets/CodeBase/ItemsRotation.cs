using UnityEngine;

namespace CodeBase
{
  public class ItemsRotation : MonoBehaviour
  {
    [SerializeField] private GameObject _item;
    [SerializeField] private float _speedRotationX;
    [SerializeField] private float _speedRotationY;
    [SerializeField] private float _speedRotationZ;

    private bool _isRotate = true;

    public void StopRotation()
    {
      _isRotate = false;
    }

    private void Update()
    {
      if (!_isRotate) return;

      if (_speedRotationX != 0)
      {
        _item.transform.Rotate(Vector3.right * _speedRotationX);
      }
      if (_speedRotationY != 0)
      {
        _item.transform.Rotate(Vector3.up * _speedRotationY);
      }
      if (_speedRotationZ != 0)
      {
        _item.transform.Rotate(Vector3.forward * _speedRotationZ);
      }
    }
  }
}