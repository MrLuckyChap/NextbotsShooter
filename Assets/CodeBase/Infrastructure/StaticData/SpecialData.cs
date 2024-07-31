using System;
using UnityEngine;

namespace CodeBase.Infrastructure.StaticData
{
  [Serializable]
  public class SpecialData
  {
    [Header("Not important")] 
    public SpaceOrientation SpaceOrientation;
    public Vector3 ColliderSize;
  }
}