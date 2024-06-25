using UnityEngine;

namespace CodeBase.Infrastructure.AssetManagement
{
  public interface IAssetProvider
  {
    GameObject Instantiate(string path, Vector3 at);
    GameObject Instantiate(string path);
    GameObject Instantiate(GameObject prefab);
    GameObject Instantiate(GameObject prefab, Vector3 position, Quaternion rotation);
  }
}