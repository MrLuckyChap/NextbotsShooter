using UnityEngine;

namespace CodeBase
{
  public class ItemsRotation : MonoBehaviour
  {
    [SerializeField] private GameObject _item;
    [SerializeField] private float _speedRotation;

    private bool _isRotate = true;

    public void StopRotation()
    {
      _isRotate = false;
    }

    private void Update()
    {
      if (!_isRotate) return;

      _item.transform.Rotate(Vector3.forward * _speedRotation);
    }
  }
}