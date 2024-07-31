using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Infrastructure.StaticData
{
  [CreateAssetMenu(fileName = "AlliesData", menuName = "Data/Allies")]
  public class AlliesData : ScriptableObject
  {
    public List<EntityData> AllAlliesData;
  }
}