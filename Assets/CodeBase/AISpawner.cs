using System.Collections.Generic;
using CodeBase.Infrastructure.StaticData;
using UnityEngine;
using UnityEngine.AI;

namespace CodeBase
{
  public class AISpawner : MonoBehaviour
  {
    [SerializeField] private float _minDistanceBetweenObjects = 2f;
    [SerializeField] private LayerMask _collisionLayers;
    [SerializeField] private LayerMask _raycastIgnoredLayers;
    [SerializeField] private List<Transform> _playerSpawnerTransform;
    [SerializeField] private List<Transform> _enemiesSpawnerTransform;
    [SerializeField] private List<Transform> _weaponSpawnerTransform;

    private NavMeshTriangulation _triangulation;
    private List<Vector3> randomPoints = new();


    public void Init()
    {
      _triangulation = NavMesh.CalculateTriangulation();
    }

    public Vector3 GetNavMeshRandomPoint()
    {
      Vector3 randomPoint = GetPoint();
      if (IsOverlappingOtherObjects(randomPoint))
      {
        GetNavMeshRandomPoint();
      }

      return randomPoint;
    }


    public Vector3 GetRandomPointInsideTransform()
    {
      Transform selectedTransform = _enemiesSpawnerTransform[Random.Range(0, _enemiesSpawnerTransform.Count)];
      // // Получить масштаб Transform
      // Vector3 localScale = selectedTransform.lossyScale;
      // Vector3 position = transform.position;
      // // Рассчитываем половину размеров по каждой оси
      // Vector3 halfScale = localScale / 2.0f;
      //
      // // Генерируем случайные координаты в пределах размеров объекта
      // float randomX = Random.Range(-halfScale.x, halfScale.x);
      // float randomY = Random.Range(-halfScale.y, halfScale.y);
      // float randomZ = Random.Range(-halfScale.z, halfScale.z);
      //
      // // Определяем случайную точку относительно центра объекта
      // Vector3 randomPoint = new Vector3(randomX, randomY, randomZ);
      //
      // // Преобразуем координаты из локальных в мировые
      // // randomPoint = transform.TransformPoint(randomPoint);
      // randomPoint += position;
      // Debug.Log($"randomWorldPoint {randomPoint}");
      // return randomPoint;
      Bounds bounds = selectedTransform.GetComponent<BoxCollider>().bounds;

      // Генерируем случайные координаты внутри границ
      float randomX = Random.Range(bounds.min.x, bounds.max.x);
      float randomY = Random.Range(bounds.min.y, bounds.max.y);
      float randomZ = Random.Range(bounds.min.z, bounds.max.z);

      // Возвращаем случайную точку
      Vector3 randomPoint = new Vector3(randomX, randomY, randomZ);
      Debug.Log($"randomWorldPoint {randomPoint}");
      return randomPoint;
      
    }

    public Vector3 GetNavMeshRandomPointFromEntityAreas(EntityType entityType)
    {
      switch (entityType)
      {
        // Transform selectedTransform = _enemiesSpawnerTransform[Random.Range(0, _enemiesSpawnerTransform.Count)];
        // Vector2 randomPoint = Random.insideUnitCircle * 5f;
        // Vector3 rayStartPoint = selectedTransform.position + new Vector3(randomPoint.x, 0f, randomPoint.y);
        case EntityType.Enemy:
        {
          Vector3 rayStartPoint = GetRandomPointInsideTransform();
          randomPoints.Add(rayStartPoint);
          Ray downRay = new Ray(rayStartPoint, Vector3.down);
          if (Physics.Raycast(downRay, out var downHit, 100f, ~_raycastIgnoredLayers))
          {
            Debug.Log($"downHit.point {downHit.point}");
            if (NavMesh.SamplePosition(downHit.point, out var navMeshHit, 5f, NavMesh.AllAreas))
            {
              Debug.Log($"navMeshHit.position {navMeshHit.position}");
              return navMeshHit.position;
            }
          }
          break;
        }
      }

      return GetNavMeshRandomPoint();
    }
    
    private Vector3 GetPoint()
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