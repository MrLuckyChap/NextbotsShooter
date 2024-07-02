using System.Collections.Generic;
using System.Linq;
using CodeBase.Infrastructure.StaticData;
using UnityEngine;

namespace CodeBase.Services.StaticData
{
  public class StaticDataService : IStaticDataService
  {
    public AllLevelsData AllLevelsData => _allLevels;

    private const string LevelsDataPath = "Data/Levels";
    private const string AllLevelsDataPath = "Data/AllLevelsData";

    private Dictionary<int, LevelData> _levels;
    private AllLevelsData _allLevels;

    public void Load()
    {
      _levels = Resources
        .LoadAll<LevelData>(LevelsDataPath)
        .ToDictionary(x => x.LevelNumber, x => x);

      _allLevels = Resources
        .Load<AllLevelsData>(AllLevelsDataPath);
    }

    public LevelData ForLevel(int level) =>
      _levels.TryGetValue(level, out LevelData staticData)
        ? staticData
        : null;
  }
}