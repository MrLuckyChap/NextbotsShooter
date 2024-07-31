namespace CodeBase.Infrastructure.States
{
  public interface IState: IExitableState
  {
    void Enter();
  }

  public interface IPayloadedState<TPayload> : IExitableState
  {
    void Enter(TPayload payload);
  }

  public interface IPayloadedState<TPayload, TLevel> : IExitableState
  {
    void Enter(TPayload payload, TLevel level);
  }
  
  public interface IExitableState
  {
    void Exit();
  }
}