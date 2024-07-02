using CodeBase.MovementGround;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

namespace CodeBase
{
  public class AISpawner : MonoBehaviour
  {
    [SerializeField] private float _minDistanceBetweenObjects = 2f;
    [SerializeField] private Ground _ground;
    [SerializeField] private LayerMask _collisionLayers;
    private NavMeshTriangulation _triangulation;

    public void Init()
    {
      // Получаем данные о треугольниках NavMesh
      _triangulation = NavMesh.CalculateTriangulation();
    }
    
    public Vector3 GetPointForInstantiate()
    {
      // List<Vector3> points = new List<Vector3>();
       Vector3 randomPoint = GetRandomPointOnNavMesh();
      if (IsOverlappingOtherObjects(randomPoint))
      {
        GetPointForInstantiate();
      }
      Debug.Log("Testing Point Find.");
      return randomPoint;
    }
    
    
    Vector3 GetRandomPointOnNavMesh()
    {
      // Выбираем случайный треугольник
      int randomTriangleIndex = Random.Range(0, _triangulation.indices.Length / 3);
      int[] triangleIndices = new int[] { 
        _triangulation.indices[randomTriangleIndex * 3],
        _triangulation.indices[randomTriangleIndex * 3 + 1],
        _triangulation.indices[randomTriangleIndex * 3 + 2] 
      };

      // Получаем вершины треугольника
      Vector3 vertex0 = _triangulation.vertices[triangleIndices[0]];
      Vector3 vertex1 = _triangulation.vertices[triangleIndices[1]];
      Vector3 vertex2 = _triangulation.vertices[triangleIndices[2]];

      // Генерируем случайную точку внутри треугольника
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


    // Метод для получения случайной точки на NavMesh
    // Vector3 GetRandomPointOnNavMesh()
    // {
    //   
    //   // Получаем границы NavMesh
    //   Bounds navMeshBounds = _ground.NavMeshSurface.navMeshData.sourceBounds;
    //
    //   for (int i = 0; i < 10; i++)
    //   {
    //     // Генерируем случайную точку внутри границ
    //     Vector3 randomPoint = new Vector3(
    //       Random.Range(navMeshBounds.min.x, navMeshBounds.max.x),
    //       navMeshBounds.center.y,
    //       Random.Range(navMeshBounds.min.z, navMeshBounds.max.z)
    //     );
    //
    //     // Проверяем, находится ли точка на NavMesh
    //     NavMeshHit hit;
    //     if (NavMesh.SamplePosition(randomPoint, out hit, 10f, _ground.gameObject.layer))
    //     {
    //       return hit.position;
    //     }
    //
    //   }
    //
    //   Debug.LogWarning("Testing Don't find point after 10 steps.");
    //   return Vector3.zero;
    // }

    // Метод для проверки перекрытия с другими объектами
    bool IsOverlappingOtherObjects(Vector3 point)
    {
      // Используем OverlapSphere для проверки на перекрытие
      Collider[] colliders = Physics.OverlapSphere(point, _minDistanceBetweenObjects, _collisionLayers);
      // if ((layerMask.value & (1 << obj.gameObject.layer)) != 0)
      // {
      // }
      // Если найдены коллайдеры (кроме самого объекта спавна), то есть перекрытие
      return colliders.Length > 0;
    }
  }
}