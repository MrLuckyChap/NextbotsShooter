using UnityEngine;
using UnityEngine.AI;

namespace CodeBase.MovementGround
{
  public class Ground : MonoBehaviour
  {
    [SerializeField] private NavMeshData _navMeshData;
    
    private void Start()
    {
      NavMesh.AddNavMeshData(_navMeshData);
    }
  }
}