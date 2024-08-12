using System.Collections.Generic;
using System.Linq;
using CodeBase.Infrastructure.StaticData;
using UnityEngine;

namespace CodeBase.Services.StaticData
{
  public class StaticDataService : IStaticDataService
  {
    public EnemiesData EnemiesData => _enemiesData;
    public AlliesData AlliesData => _alliesData;
    public OtherObjectsData OtherObjectsData => _otherObjectsData;
    public AllLevelsData AllLevelsData => _allLevels;

    private const string LevelsDataPath = "Data/Maps";
    private const string AllLevelsDataPath = "Data/AllLevelsData";
    private const string EnemiesDataPath = "Data/EnemiesData";
    private const string AlliesDataPath = "Data/AlliesData";
    private const string OtherObjectsDataPath = "Data/OtherObjectsData";

    private Dictionary<int, LevelData> _levels;
    private AllLevelsData _allLevels;
    private EnemiesData _enemiesData;
    private AlliesData _alliesData;
    private OtherObjectsData _otherObjectsData;

    public void Load()
    {
      _levels = Resources
        .LoadAll<LevelData>(LevelsDataPath)
        .ToDictionary(x => x.LevelNumber, x => x);

      _allLevels = Resources
        .Load<AllLevelsData>(AllLevelsDataPath);
      _enemiesData = Resources
        .Load<EnemiesData>(EnemiesDataPath);
      _alliesData = Resources
        .Load<AlliesData>(AlliesDataPath);
      _otherObjectsData = Resources
        .Load<OtherObjectsData>(OtherObjectsDataPath);
    }

    public LevelData ForLevel(int level) =>
      _levels.TryGetValue(level, out LevelData staticData)
        ? staticData
        : null;
  }
}