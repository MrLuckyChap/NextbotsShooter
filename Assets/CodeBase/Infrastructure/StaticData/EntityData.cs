using System;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Infrastructure.StaticData
{
  [Serializable]
  public class EntityData
  {
    [Header("Important fill")]
    public string Id;
    public string Name;
    public SpawnType Type;
    public Sprite EntityImage;
    public GameObject EntityPrefab;
    public SpecialData Special;
  }
  
  public enum SpaceOrientation
  {
    None,
    TwoD,
    ThreeD
  }
  
  public enum SpawnType
  {
    Enemy,
    Ally,
    Environment
  }
}