using System;
using UnityEngine;

namespace CodeBase.Infrastructure.StaticData
{
  [Serializable]
  public class EntityData
  {
    public string Id;
    public string Name;
    public EntityType Type;
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
  
  public enum EntityType
  {
    Enemy,
    Ally,
    Environment
  }
}