using System.Threading.Tasks;
using CodeBase.Infrastructure.AssetManagement;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
  public class GameFactory : IGameFactory
  {
    public GameObject PlayerGameObject { get; private set; }

    private readonly IAssetProvider _assets;

    public GameFactory(IAssetProvider assets)
    {
      _assets = assets;
    }

    public async Task<GameObject> CreatePlayer(Vector3 at) => PlayerGameObject = _assets.Instantiate(AssetPath.PlayerPath, at);

    public async Task<GameObject> CreateHud() => _assets.Instantiate(AssetPath.HudPath);

    public async Task<GameObject> CreateGameObject(GameObject prefab) => _assets.Instantiate(prefab);

    public async Task<GameObject> CreateGameObject(GameObject prefab, Vector3 position, Quaternion rotation) =>
      _assets.Instantiate(prefab, position, rotation);
  }
}