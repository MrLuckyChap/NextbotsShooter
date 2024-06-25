using System.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
  public interface IGameFactory
  {
    Task<GameObject> CreatePlayer(Vector3 at);
    Task<GameObject> CreateHud();
    Task<GameObject> CreateGameObject(GameObject prefab);
    Task<GameObject> CreateGameObject(GameObject prefab, Vector3 position, Quaternion rotation);
    GameObject PlayerGameObject { get; }
  }
}