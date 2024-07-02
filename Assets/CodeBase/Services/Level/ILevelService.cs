using System;

namespace CodeBase.Services.Level
{
  public interface ILevelService
  {
    event Action LevelCompleted;
    event Action LastLevelPointPassed;
    event Action PlayButtonClicked;
    
    void NotifyListenersAboutPassedLastPoint();
    void RestartLevel();
    void StartLevel();
  }
}