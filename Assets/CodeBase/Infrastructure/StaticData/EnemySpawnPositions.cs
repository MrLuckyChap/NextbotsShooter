using System;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Infrastructure.StaticData
{
  [Serializable]
  public class EnemySpawnPositions
  {
    public List<Vector3> Positions;

    public EnemySpawnPositions(Vector3 position)
    {
      Positions.Add(position);
    }
  }
}