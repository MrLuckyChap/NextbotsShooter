using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Infrastructure.StaticData
{
  [CreateAssetMenu(fileName = "OtherObjectsData", menuName = "Data/OtherObjects")]
  public class OtherObjectsData : ScriptableObject
  {
    public List<EntityData> AllOtherObjectsData;
  }
}