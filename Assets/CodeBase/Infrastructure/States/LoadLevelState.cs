using System.Threading.Tasks;
using All_Imported_Assets.AMFPC.Camera.Scripts;
using All_Imported_Assets.AMFPC.Enemy.Scripts;
using All_Imported_Assets.AMFPC.First_Person_Items___Arms.Scripts;
using All_Imported_Assets.AMFPC.Input.Scripts;
using All_Imported_Assets.AMFPC.Interactables;
using All_Imported_Assets.AMFPC.Player_Controller.Scripts;
using All_Imported_Assets.AMFPC.UI.Scripts;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.PoolObject;
using CodeBase.Infrastructure.StaticData;
using CodeBase.MovementGround;
using CodeBase.Services.StaticData;
using CodeBase.Services.UI;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
  public class LoadLevelState : IPayloadedState<string, int>
  {
    private readonly GameStateMachine _stateMachine;
    private readonly SceneLoader _sceneLoader;
    private readonly LoadingCurtain _loadingCurtain;
    private readonly IGameFactory _gameFactory;
    private readonly IStaticDataService _dataService;
    private readonly IGameUIService _gameUIService;
    private int _level;

    public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingCurtain loadingCurtain,
      IGameFactory gameFactory, IStaticDataService dataService, IGameUIService gameUIService)
    {
      _stateMachine = gameStateMachine;
      _sceneLoader = sceneLoader;
      _loadingCurtain = loadingCurtain;
      _gameFactory = gameFactory;
      _dataService = dataService;
      _gameUIService = gameUIService;
    }

    public void Enter(string sceneName, int level)
    {
      _loadingCurtain.Show();
      _level = level;
      // Debug.Log("Testing LoadLevelState Enter");
      _sceneLoader.Load(sceneName, OnLoaded);
    }

    public void Exit()
    {
      _loadingCurtain.Hide();
      // Debug.Log("Testing LoadLevelState Exit");
    }

    private async void OnLoaded()
    {
      // Debug.Log("Testing LoadLevelState OnLoaded Start");
      _dataService.Load();
      await InitGameWorld();

      _stateMachine.Enter<GameState>();
    }

    private async Task InitGameWorld()
    {
      GameObject level = await InitLocation();
      level.GetComponent<Ground>().UpdateNavMesh(_dataService.ForLevel(_level).NavMeshData);
      AISpawner aiSpawner = level.GetComponent<AISpawner>();
      aiSpawner.Init();
      PlayerController playerController = await InitPlayer(aiSpawner);
      GameObject playerCamera = await InitPlayerCamera(playerController.transform.position);
      InputManager inputManager = await InitInputManager();
      ItemManager itemManager = playerCamera.GetComponentInChildren<ItemManager>();
      GameObject gameHud = await InitGameHud();
      await InitEnemies(aiSpawner, playerController.transform);
      await InitWeapons(aiSpawner, itemManager, playerController.transform.position);
      MonoBehaviourPool<EnemyController> enemyPool = _gameFactory.CreateEnemy2DPool(
        _dataService.ForLevel(_level).Enemy[0].GetComponent<EnemyController>(), aiSpawner.GetNavMeshRandomPoint(), 20);
      DevModeSpawnPosition devModeSpawnPosition = playerCamera.GetComponentInChildren<DevModeSpawnPosition>();
      _gameUIService.Init(aiSpawner, devModeSpawnPosition, playerController.transform, enemyPool);

      // EnemyController pool = await 
      playerController.inputManager = inputManager;
      playerController.GetComponent<PlayerRespawn>().AISpawner = aiSpawner;
      UIManager uiManager = gameHud.GetComponentInChildren<UIManager>();
      CameraRayCast cameraRayCast = playerCamera.GetComponentInChildren<CameraRayCast>();
      cameraRayCast.UIReference = uiManager;
      playerCamera.GetComponentInChildren<CameraLook>().PlayerController = playerController;
      itemManager.playerController = playerController;
      itemManager.UIReference = uiManager;
      // Debug.Log("Testing LoadLevelState InitGameWorld Complete");
    }

    private async Task<GameObject> InitLocation() => await _gameFactory.CreateGameObject(_dataService.ForLevel(_level).Location);

    private async Task<PlayerController> InitPlayer(AISpawner aiSpawner)
    {
      Vector3 spawnPoint = aiSpawner.GetNavMeshRandomPoint();
      spawnPoint.y += 1f;
      GameObject playerController = await _gameFactory.CreateGameObject(
        _dataService.AllLevelsData.PlayerController, spawnPoint,
        new Quaternion(0f, 0f, 0f, 0f));
      // var transformPosition = playerTransform.transform.position;
      // transformPosition.y += 20f;
      return playerController.GetComponent<PlayerController>();
    }

    private async Task<GameObject> InitPlayerCamera(Vector3 playerPosition) => await _gameFactory.CreateGameObject(
      _dataService.AllLevelsData.PlayerCamera, playerPosition, new Quaternion(0f, 0f, 0f, 0f));

    private async Task InitEnemies(AISpawner aiSpawner, Transform playerTransform)
    {
      for (int i = 0; i < _dataService.ForLevel(_level).EnemyCount; i++)
      {
        var gameObject = _gameFactory.CreateGameObject(_dataService.ForLevel(_level).Enemy[0], aiSpawner.GetNavMeshRandomPointFromEntityAreas(EntityType.Enemy),
          new Quaternion(0f, 0f, 0f, 0f));
        gameObject.Result.GetComponent<EnemyController>().Init(aiSpawner, playerTransform);
        await gameObject;
      }

      // foreach (EnemySpawnPositions points in _dataService.ForLevel(_level).EnemySpawnPositions)
      //   foreach (Vector3 position in points.Positions)
      //     await _gameFactory.CreateGameObjects(_dataService.ForLevel(_level).Enemy, position, new Quaternion(0f, 180f, 0f, 0f));
    }

    private async Task<InputManager> InitInputManager()
    {
      GameObject inputManager = await _gameFactory.CreateGameObject(_dataService.AllLevelsData.InputManager);
      return inputManager.GetComponent<InputManager>();
    }

    private async Task<GameObject> InitGameHud()
    {
      return await _gameFactory.CreateGameObject(_dataService.AllLevelsData.GameHud);
    }

    private async Task InitWeapons(AISpawner aiSpawner, ItemManager itemManager, Vector3 playerPosition)
    {
     GameObject weapon = await _gameFactory.CreateGameObject(
        _dataService.AllLevelsData.AK47, aiSpawner.GetNavMeshRandomPoint(),
        new Quaternion(0f, 0f, 0f, 0f));
     weapon.GetComponentInChildren<PickupItem>().ItemManager = itemManager;
     
     GameObject weapon1 = await _gameFactory.CreateGameObject(
       _dataService.AllLevelsData.AK47, aiSpawner.GetNavMeshRandomPoint(),
       new Quaternion(0f, 0f, 0f, 0f));
     weapon1.GetComponentInChildren<PickupItem>().ItemManager = itemManager;
     
     GameObject weapon2 = await _gameFactory.CreateGameObject(
       _dataService.AllLevelsData.AK47, aiSpawner.GetNavMeshRandomPoint(),
       new Quaternion(0f, 0f, 0f, 0f));
     weapon2.GetComponentInChildren<PickupItem>().ItemManager = itemManager;
     
     GameObject weapon3 = await _gameFactory.CreateGameObject(
       _dataService.AllLevelsData.Shotgun, aiSpawner.GetNavMeshRandomPoint(),
       new Quaternion(0f, 0f, 0f, 0f));
     weapon3.GetComponentInChildren<PickupItem>().ItemManager = itemManager;
     
     GameObject weapon4 = await _gameFactory.CreateGameObject(
       _dataService.AllLevelsData.Shotgun, aiSpawner.GetNavMeshRandomPoint(),
       new Quaternion(0f, 0f, 0f, 0f));
     weapon4.GetComponentInChildren<PickupItem>().ItemManager = itemManager;
     
     GameObject weapon5 = await _gameFactory.CreateGameObject(
       _dataService.AllLevelsData.Pistol, playerPosition,
       new Quaternion(0f, 0f, 0f, 0f));
     weapon5.GetComponentInChildren<PickupItem>().ItemManager = itemManager;
    }
  }
}