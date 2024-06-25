using System.Collections.Generic;
using System.Linq;
using CodeBase.Infrastructure.StaticData;
using UnityEngine;

namespace CodeBase.Services.StaticData
{
  public class StaticDataService : IStaticDataService
  {
    private const string LevelsDataPath = "StaticData/Levels";

    private Dictionary<int, LevelStaticData> _levels;

    public void Load()
    {
      _levels = Resources
        .LoadAll<LevelStaticData>(LevelsDataPath)
        .ToDictionary(x => x.LevelNumber, x => x);
    }

    public LevelStaticData ForLevel(int level) =>
      _levels.TryGetValue(level, out LevelStaticData staticData)
        ? staticData
        : null;
  }
}