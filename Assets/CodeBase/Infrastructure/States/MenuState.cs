using CodeBase.Services.Level;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
  public class MenuState : IPayloadedState<string>
  {
    private readonly GameStateMachine _stateMachine;
    private readonly SceneLoader _sceneLoader;
    private readonly LoadingCurtain _loadingCurtain;
    private readonly ILevelService _levelService;
    private string _gameSceneName;
    // private const string Game = "Game";

    public MenuState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingCurtain loadingCurtain,
      ILevelService levelService)
    {
      _levelService = levelService;
      _loadingCurtain = loadingCurtain;
      _sceneLoader = sceneLoader;
      _stateMachine = gameStateMachine;
    }

    public void Enter(string sceneName)
    {
      // Debug.Log($"Testing MenuState Enter sceneName {sceneName}");
      _gameSceneName = sceneName;
      _levelService.PlayButtonClicked += OnPlayButtonClicked;
    }

    public void Exit()
    {
      // Debug.Log("Testing MenuState Exit sceneName");
      _levelService.PlayButtonClicked -= OnPlayButtonClicked;
    }

    private void OnPlayButtonClicked(int level)
    {
      // Debug.Log($"Testing MenuState OnPlayButtonClicked _gameSceneName {_gameSceneName}");
      _stateMachine.Enter<LoadLevelState, string, int>(_gameSceneName, level);
    }
  }
}