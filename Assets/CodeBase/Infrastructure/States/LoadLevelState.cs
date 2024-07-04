using System.Threading.Tasks;
using All_Imported_Assets.AMFPC.Camera.Scripts;
using All_Imported_Assets.AMFPC.Enemy.Scripts;
using All_Imported_Assets.AMFPC.First_Person_Items___Arms.Scripts;
using All_Imported_Assets.AMFPC.Input.Scripts;
using All_Imported_Assets.AMFPC.Interactables;
using All_Imported_Assets.AMFPC.Player_Controller.Scripts;
using All_Imported_Assets.AMFPC.UI.Scripts;
using CodeBase.Infrastructure.Factory;
using CodeBase.MovementGround;
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
      Debug.Log("Testing LoadLevelState Enter");
      _sceneLoader.Load(sceneName, OnLoaded);
    }

    public void Exit()
    {
      _loadingCurtain.Hide();
      Debug.Log("Testing LoadLevelState Exit");
    }

    private async void OnLoaded()
    {
      Debug.Log("Testing LoadLevelState OnLoaded Start");
      _dataService.Load();
      await InitGameWorld();

      _stateMachine.Enter<GameState>();
    }

    private async Task InitGameWorld()
    {
      GameObject level = await InitLocation();
      level.GetComponent<Ground>().UpdateNavMesh(_dataService.ForLevel(1).NavMeshData);
      AISpawner aiSpawner = level.GetComponent<AISpawner>();
      aiSpawner.Init();
      PlayerController playerController = await InitPlayer(aiSpawner);
      GameObject playerCamera = await InitPlayerCamera(playerController.transform.position);
      InputManager inputManager = await InitInputManager();
      ItemManager itemManager = playerCamera.GetComponentInChildren<ItemManager>();
      GameObject gameHud = await InitGameHud();
      await InitEnemies(aiSpawner);
      await InitWeapons(aiSpawner, itemManager);

      playerController.inputManager = inputManager;
      playerController.GetComponent<PlayerRespawn>().AISpawner = aiSpawner;
      UIManager uiManager = gameHud.GetComponentInChildren<UIManager>();
      playerCamera.GetComponentInChildren<CameraRayCast>().UIReference = uiManager;
      playerCamera.GetComponentInChildren<CameraLook>().PlayerController = playerController;
      itemManager.playerController = playerController;
      itemManager.UIReference = uiManager;
      Debug.Log("Testing LoadLevelState InitGameWorld Complete");
    }


    // private void SetPlayerPosition(Vector3 spawnPosition)
    // {
    //   GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().transform.position = spawnPosition;
    // }

    private async Task<GameObject> InitLocation() => await _gameFactory.CreateGameObject(_dataService.ForLevel(1).Location);

    private async Task<PlayerController> InitPlayer(AISpawner aiSpawner)
    {
      Vector3 spawnPoint = aiSpawner.GetPointForInstantiate();
      spawnPoint.y += 1f;
      GameObject playerController = await _gameFactory.CreateGameObject(
        _dataService.AllLevelsData.PlayerController, spawnPoint,
        new Quaternion(0f, 0f, 0f, 0f));
      // var transformPosition = playerController.transform.position;
      // transformPosition.y += 20f;
      return playerController.GetComponent<PlayerController>();
    }

    private async Task<GameObject> InitPlayerCamera(Vector3 playerPosition) => await _gameFactory.CreateGameObject(
      _dataService.AllLevelsData.PlayerCamera, playerPosition, new Quaternion(0f, 0f, 0f, 0f));

    private async Task InitEnemies(AISpawner aiSpawner)
    {
      for (int i = 0; i < _dataService.ForLevel(1).EnemyCount; i++)
      {
        var gameObject = _gameFactory.CreateGameObject(_dataService.ForLevel(1).Enemy[0], aiSpawner.GetPointForInstantiate(),
          new Quaternion(0f, 0f, 0f, 0f));
        gameObject.Result.GetComponent<EnemyController>().Init(aiSpawner);
        await gameObject;
      }

      // foreach (EnemySpawnPositions points in _dataService.ForLevel(1).EnemySpawnPositions)
      //   foreach (Vector3 position in points.Positions)
      //     await _gameFactory.CreateGameObjects(_dataService.ForLevel(1).Enemy, position, new Quaternion(0f, 180f, 0f, 0f));
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

    private async Task InitWeapons(AISpawner aiSpawner, ItemManager itemManager)
    {
     GameObject weapon = await _gameFactory.CreateGameObject(
        _dataService.AllLevelsData.AK47, aiSpawner.GetPointForInstantiate(),
        new Quaternion(0f, 0f, 0f, 0f));
     weapon.GetComponentInChildren<PickupItem>().ItemManager = itemManager;
     
     GameObject weapon1 = await _gameFactory.CreateGameObject(
       _dataService.AllLevelsData.AK47, aiSpawner.GetPointForInstantiate(),
       new Quaternion(0f, 0f, 0f, 0f));
     weapon1.GetComponentInChildren<PickupItem>().ItemManager = itemManager;
     
     GameObject weapon2 = await _gameFactory.CreateGameObject(
       _dataService.AllLevelsData.AK47, aiSpawner.GetPointForInstantiate(),
       new Quaternion(0f, 0f, 0f, 0f));
     weapon2.GetComponentInChildren<PickupItem>().ItemManager = itemManager;
     
     GameObject weapon3 = await _gameFactory.CreateGameObject(
       _dataService.AllLevelsData.Shotgun, aiSpawner.GetPointForInstantiate(),
       new Quaternion(0f, 0f, 0f, 0f));
     weapon3.GetComponentInChildren<PickupItem>().ItemManager = itemManager;
     
     GameObject weapon4 = await _gameFactory.CreateGameObject(
       _dataService.AllLevelsData.Shotgun, aiSpawner.GetPointForInstantiate(),
       new Quaternion(0f, 0f, 0f, 0f));
     weapon4.GetComponentInChildren<PickupItem>().ItemManager = itemManager;
     
     // GameObject ammo = await _gameFactory.CreateGameObject(
     //    _dataService.AllLevelsData.AKAmmo, aiSpawner.GetPointForInstantiate(),
     //    new Quaternion(0f, 0f, 0f, 0f));
     // ammo.GetComponent<PickupItem>().ItemManager = itemManager;
    }

    // private void CameraFollow(GameObject player) => Camera.main.GetComponent<FirstPersonCamera>().Follow(player);
  }
}