using CodeBase.Services.Level;

namespace CodeBase.Infrastructure.States
{
  public class GameState : IState
  {
    private ILevelService _levelService;
    private GameStateMachine _stateMachine;

    public GameState(GameStateMachine stateMachine, ILevelService levelService)
    {
      _stateMachine = stateMachine;
      _levelService = levelService;
    }

    public void Enter()
    {
      _levelService.LevelCompleted += OnLevelCompleted;
    }

    public void Exit()
    {
      _levelService.LevelCompleted -= OnLevelCompleted;
    }

    private void OnLevelCompleted()
    {
      _stateMachine.Enter<BootstrapState>();
    }
  }
}