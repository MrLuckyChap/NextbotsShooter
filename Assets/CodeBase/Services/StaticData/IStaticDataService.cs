using CodeBase.Infrastructure.StaticData;

namespace CodeBase.Services.StaticData
{
  public interface IStaticDataService
  {
    void Load();
    LevelStaticData ForLevel(int level);
  }
}