using System.Threading.Tasks;
using CodeBase.CameraLogic;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.StaticData;
using CodeBase.Services.StaticData;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
  public class LoadLevelState : IPayloadedState<string>
  {
    private readonly GameStateMachine _stateMachine;
    private readonly SceneLoader _sceneLoader;
    private readonly LoadingCurtain _loadingCurtain;
    private readonly IGameFactory _gameFactory;
    private readonly IStaticDataService _dataService;

    public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingCurtain loadingCurtain,
      IGameFactory gameFactory, IStaticDataService dataService)
    {
      _stateMachine = gameStateMachine;
      _sceneLoader = sceneLoader;
      _loadingCurtain = loadingCurtain;
      _gameFactory = gameFactory;
      _dataService = dataService;
    }

    public void Enter(string sceneName)
    {
      _loadingCurtain.Show();
      _sceneLoader.Load(sceneName, OnLoaded);
    }

    public void Exit() =>
      _loadingCurtain.Hide();

    private async void OnLoaded()
    {
      _dataService.Load();
      await InitGameWorld();

      _stateMachine.Enter<GameState>();
    }

    private async Task InitGameWorld()
    {
      GameObject level = await InitLocation();
      AISpawner aiSpawner = level.GetComponent<AISpawner>();
      GameObject player = await InitPlayer(aiSpawner);
      await InitEnemies(aiSpawner);
      GameObject hud = await InitHud();
      // CameraFollow(player);
    }

    private async Task<GameObject> InitLocation() => await _gameFactory.CreateGameObject(_dataService.ForLevel(1).Location);

    private async Task<GameObject> InitPlayer(AISpawner aiSpawner) =>
      await _gameFactory.CreatePlayer(aiSpawner.GetPointsForInstantiate());

    private async Task InitEnemies(AISpawner aiSpawner)
    {
      for (int i = 0; i < _dataService.ForLevel(1).EnemyCount; i++)
        await _gameFactory.CreateGameObject(_dataService.ForLevel(1).Enemy[0], aiSpawner.GetPointsForInstantiate(),
          new Quaternion(0f, 180f, 0f, 0f));
      // foreach (EnemySpawnPositions points in _dataService.ForLevel(1).EnemySpawnPositions)
      //   foreach (Vector3 position in points.Positions)
      //     await _gameFactory.CreateGameObject(_dataService.ForLevel(1).Enemy, position, new Quaternion(0f, 180f, 0f, 0f));
    }

    private async Task<GameObject> InitHud() => await _gameFactory.CreateHud();
    
    private void CameraFollow(GameObject player) => Camera.main.GetComponent<FirstPersonCamera>().Follow(player);
  }
}