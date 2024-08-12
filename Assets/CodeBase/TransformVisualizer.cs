using UnityEngine;

namespace CodeBase
{
  public class TransformVisualizer : MonoBehaviour
  {
    [SerializeField] private Color _playerSpawner;

    private void OnDrawGizmos()
    {
      Gizmos.color = _playerSpawner;
      Transform[] spawnerTransform = GetComponentsInChildren<Transform>();
      foreach (Transform child in spawnerTransform) Gizmos.DrawCube(child.position, child.localScale);
    }
  }
}