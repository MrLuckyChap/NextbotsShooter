using System;

namespace CodeBase.Services.Level
{
  public interface ILevelService
  {
    event Action LevelCompleted;
    event Action LastLevelPointPassed;
    
    void NotifyListenersAboutPassedLastPoint();
    void RestartLevel();
  }
}