namespace CodeBase.Infrastructure.States
{
  public class BootstrapState : IState
  {
    private const string Menu = "Menu";
    private const string Game = "Game";
    private readonly GameStateMachine _stateMachine;
    private readonly SceneLoader _sceneLoader;
    private readonly LoadingCurtain _loadingCurtain;

    public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingCurtain loadingCurtain)
    {
      _loadingCurtain = loadingCurtain;
      _stateMachine = stateMachine;
      _sceneLoader = sceneLoader;
    }

    public void Enter()
    {
      _loadingCurtain.Show();
      _sceneLoader.Load(Menu, EnterLoadLevel);
    }

    public void Exit()
    {
      _loadingCurtain.Hide();
    }

    private void EnterLoadLevel() =>
      _stateMachine.Enter<MenuState, string>(Game);
  }
}