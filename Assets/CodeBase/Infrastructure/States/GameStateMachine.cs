using System;
using System.Collections.Generic;
using CodeBase.Infrastructure.Factory;
using CodeBase.Services.Level;
using CodeBase.Services.StaticData;
using CodeBase.Services.UI;
using Zenject;

namespace CodeBase.Infrastructure.States
{
  public class GameStateMachine : IInitializable
  {
    private Dictionary<Type, IExitableState> _states;
    private IExitableState _activeState;

    public GameStateMachine(SceneLoader sceneLoader, LoadingCurtain loadingCurtain, IGameFactory gameFactory,
      IStaticDataService dataService, ILevelService levelService, IGameUIService gameUIService)
    {
      _states = new Dictionary<Type, IExitableState>
      {
        [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader, loadingCurtain),
        [typeof(MenuState)] = new MenuState(this, sceneLoader, loadingCurtain, levelService),
        [typeof(LoadLevelState)] = new LoadLevelState(this, sceneLoader, loadingCurtain, gameFactory, dataService, gameUIService),
        [typeof(GameState)] = new GameState(this, levelService),
      };
    }

    public void Initialize()
    {
      Enter<BootstrapState>();
    }

    public void Enter<TState>() where TState : class, IState
    {
      IState state = ChangeState<TState>();
      state.Enter();
    }

    public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
    {
      TState state = ChangeState<TState>();
      state.Enter(payload);
    }

    public void Enter<TState, TPayload, TLevel>(TPayload payload, TLevel level) where TState : class, IPayloadedState<TPayload, TLevel>
    {
      TState state = ChangeState<TState>();
      state.Enter(payload, level);
    }

    private TState ChangeState<TState>() where TState : class, IExitableState
    {
      _activeState?.Exit();

      TState state = GetState<TState>();
      _activeState = state;

      return state;
    }

    private TState GetState<TState>() where TState : class, IExitableState =>
      _states[typeof(TState)] as TState;
  }
}