using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Infrastructure.StaticData
{
  [CreateAssetMenu(fileName = "EnemiesData", menuName = "Data/Enemies")]
  public class EnemiesData : ScriptableObject
  {
    public List<EntityData> AllEnemiesData;
  }
}