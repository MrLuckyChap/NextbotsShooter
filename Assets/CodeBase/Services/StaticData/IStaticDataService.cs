using CodeBase.Infrastructure.StaticData;

namespace CodeBase.Services.StaticData
{
  public interface IStaticDataService
  {
    AllLevelsData AllLevelsData { get; }
    void Load();
    LevelData ForLevel(int level);
  }
}