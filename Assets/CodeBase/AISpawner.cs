using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace CodeBase
{
  public class AISpawner : MonoBehaviour
  {
    // Префаб объекта, который нужно инстанциировать
    [SerializeField] private GameObject objectToSpawn;

    // Ссылка на NavMeshData
    [SerializeField] private NavMeshData navMeshData;


    // Радиус вокруг точки спавна, в котором будет искаться точка на NavMesh
    [SerializeField] private float spawnRadius = 5f;

    // Минимальное расстояние между объектами
    [SerializeField] private float minDistanceBetweenObjects = 2f;

    // Метод для инстанциирования объектов
    public Vector3 GetPointsForInstantiate()
    {
      // List<Vector3> points = new List<Vector3>();
      Vector3 randomPoint = GetRandomPointOnNavMesh(transform.position, spawnRadius);
      if (IsOverlappingOtherObjects(randomPoint))
      {
        GetPointsForInstantiate();
      }
      
      return randomPoint;
      // for (int i = 0; i < spawnCount; i++)
      // {
      //     // Генерируем случайную точку на NavMesh с проверкой на перекрытие
      //     randomPoint = GetRandomPointOnNavMesh(transform.position, spawnRadius);
      //     
      //     // Проверяем, не пересекается ли новый объект с уже существующими
      //     if (IsOverlappingOtherObjects(randomPoint))
      //     {
      //         // Если пересекается, уменьшаем счетчик и пробуем снова
      //         i--; 
      //         continue;
      //     }
      //     // points.Add(randomPoint);
      //     // Инстанциируем объект в случайной точке
      //     // Instantiate(objectToSpawn, randomPoint, Quaternion.identity);
      // }
    }

    // Метод для получения случайной точки на NavMesh
    Vector3 GetRandomPointOnNavMesh(Vector3 center, float radius)
    {
      for (int i = 0; i < 10; i++) // Попытки найти точку
      {
        Vector3 randomPoint = Random.insideUnitSphere * radius + center;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, radius, NavMesh.AllAreas))
        {
          return hit.position;
        }
      }

      Debug.LogWarning("Не удалось найти точку на NavMesh после 10 попыток.");
      return center; // Возвращаем центр, если не удалось найти
    }

    // Метод для проверки перекрытия с другими объектами
    bool IsOverlappingOtherObjects(Vector3 point)
    {
      // Используем OverlapSphere для проверки на перекрытие
      Collider[] colliders = Physics.OverlapSphere(point, minDistanceBetweenObjects);
      // Если найдены коллайдеры (кроме самого объекта спавна), то есть перекрытие
      return colliders.Length > 0;
    }
  }
}