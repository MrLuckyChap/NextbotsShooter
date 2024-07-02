using System;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

namespace CodeBase.MovementGround
{
  public class Ground : MonoBehaviour
  {
    // [SerializeField] private NavMeshSurface _navMeshSurface;
    // [SerializeField] private NavMeshData _navMeshData;
    public NavMeshSurface NavMeshSurface { get; set; } 
    
    private void Start()
    {
      // NavMesh.AddNavMeshData(_navMeshData);
    }

    public void UpdateNavMesh(NavMeshData navMeshData)
    {
      NavMeshSurface = gameObject.AddComponent<NavMeshSurface>();
      NavMeshSurface.navMeshData = navMeshData;
      NavMeshSurface.AddData();
      // _navMeshSurface.UpdateNavMesh(navMeshData);
    }
  }
}