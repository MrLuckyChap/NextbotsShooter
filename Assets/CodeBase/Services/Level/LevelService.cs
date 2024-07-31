using System;

namespace CodeBase.Services.Level
{
  public class LevelService : ILevelService
  {
    public event Action LevelCompleted;
    public event Action LastLevelPointPassed;
    public event Action<int> PlayButtonClicked;

    public void NotifyListenersAboutPassedLastPoint() => LastLevelPointPassed?.Invoke();
    public void RestartLevel() => LevelCompleted?.Invoke();
    public void StartLevel(int level)
    {
      PlayButtonClicked?.Invoke(level);
    }
  }
}