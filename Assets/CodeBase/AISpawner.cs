using UnityEngine;
using UnityEngine.AI;

namespace CodeBase
{
  public class AISpawner : MonoBehaviour
  {
    [SerializeField] private float _minDistanceBetweenObjects = 2f;
    [SerializeField] private LayerMask _collisionLayers;
    private NavMeshTriangulation _triangulation;

    public void Init()
    {
      _triangulation = NavMesh.CalculateTriangulation();
    }

    public Vector3 GetPointForInstantiate()
    {
      Vector3 randomPoint = GetRandomPointOnNavMesh();
      if (IsOverlappingOtherObjects(randomPoint))
      {
        GetPointForInstantiate();
      }

      return randomPoint;
    }

    private Vector3 GetRandomPointOnNavMesh()
    {
      int randomTriangleIndex = Random.Range(0, _triangulation.indices.Length / 3);
      int[] triangleIndices = new int[]
      {
        _triangulation.indices[randomTriangleIndex * 3],
        _triangulation.indices[randomTriangleIndex * 3 + 1],
        _triangulation.indices[randomTriangleIndex * 3 + 2]
      };

      Vector3 vertex0 = _triangulation.vertices[triangleIndices[0]];
      Vector3 vertex1 = _triangulation.vertices[triangleIndices[1]];
      Vector3 vertex2 = _triangulation.vertices[triangleIndices[2]];

      float u = Random.Range(0f, 1f);
      float v = Random.Range(0f, 1f);
      if (u + v > 1f)
      {
        u = 1f - u;
        v = 1f - v;
      }

      Vector3 randomPoint = vertex0 + (vertex1 - vertex0) * u + (vertex2 - vertex0) * v;

      return randomPoint;
    }


    private bool IsOverlappingOtherObjects(Vector3 point)
    {
      Collider[] colliders = Physics.OverlapSphere(point, _minDistanceBetweenObjects, _collisionLayers);
      // if ((layerMask.value & (1 << obj.gameObject.layer)) != 0)
      // {
      // }

      return colliders.Length > 0;
    }
  }
}