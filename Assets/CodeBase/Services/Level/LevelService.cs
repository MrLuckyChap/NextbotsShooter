using System;

namespace CodeBase.Services.Level
{
  public class LevelService : ILevelService
  {
    public event Action LevelCompleted;
    public event Action LastLevelPointPassed;
    
    public void NotifyListenersAboutPassedLastPoint() => LastLevelPointPassed?.Invoke();
    public void RestartLevel() => LevelCompleted?.Invoke();
  }
}