using UnityEngine;
using UnityEngine.AI;

namespace All_Imported_Assets.AMFPC.Camera.Scripts
{
  public class DevModeSpawnPosition : MonoBehaviour
  {
    [SerializeField] private LayerMask _devModeRaycastIgnoredLayers;
    [SerializeField] private float _devModeSpawnDistance;
    private UnityEngine.Camera _cameraMain;
    private Vector3 _rayPosition;

    private void Awake()
    {
      _cameraMain = UnityEngine.Camera.main;
      _rayPosition = new Vector3(0.5f, 0.5f, 0);
    }

    public Vector3 GetDevModeAgentSpawnPoint()
    {
      Ray ray = _cameraMain.ViewportPointToRay(_rayPosition);
      while (_devModeSpawnDistance >= 0.25f)
      {
        Physics.Raycast(ray, out var raycastHit, _devModeSpawnDistance, ~_devModeRaycastIgnoredLayers);
        if(raycastHit.collider == null)
        {
          Vector3 vector3 = ray.GetPoint(_devModeSpawnDistance);
          Ray downRay = new Ray(vector3, Vector3.down);
          if (Physics.Raycast(downRay, out var downHit, 100f, ~_devModeRaycastIgnoredLayers))
          {
            if (NavMesh.SamplePosition(downHit.point, out var navMeshHit, 20f, ~_devModeRaycastIgnoredLayers))
            {
              return navMeshHit.position;
            }

            // Vector3 hitInfoPoint = downHit.point;
            // return hitInfoPoint;
          }
        }
        _devModeSpawnDistance -= 0.25f;
      }

      Ray downRay2 = new Ray(ray.GetPoint(_devModeSpawnDistance - _devModeSpawnDistance / 2), Vector3.down);
      Physics.Raycast(downRay2, out var downHit2, 100f, ~_devModeRaycastIgnoredLayers);
      NavMesh.SamplePosition(downHit2.point, out var navMeshHit2, 20f, NavMesh.AllAreas);
      return navMeshHit2.position;
      // Vector3 downHit2Point = downHit2.point;
      // return downHit2Point;
    }
    
    public Vector3 GetDevModeSpawnPoint()
    {
      Ray ray = _cameraMain.ViewportPointToRay(_rayPosition);
      while (_devModeSpawnDistance >= 0.25f)
      {
        Physics.Raycast(ray, out var raycastHit, _devModeSpawnDistance, ~_devModeRaycastIgnoredLayers);
        if(raycastHit.collider == null)
        {
          Vector3 vector3 = ray.GetPoint(_devModeSpawnDistance);
          Ray downRay = new Ray(vector3, Vector3.down);
          if (Physics.Raycast(downRay, out var downHit, 100f, ~_devModeRaycastIgnoredLayers))
          {
            // if (NavMesh.SamplePosition(downHit.point, out var navMeshHit, 20f, ~_devModeRaycastIgnoredLayers))
            // {
            //   return navMeshHit.position;
            // }

            Vector3 hitInfoPoint = downHit.point;
            return hitInfoPoint;
          }
        }
        _devModeSpawnDistance -= 0.25f;
      }

      Ray downRay2 = new Ray(ray.GetPoint(_devModeSpawnDistance - _devModeSpawnDistance / 2), Vector3.down);
      Physics.Raycast(downRay2, out var downHit2, 100f, ~_devModeRaycastIgnoredLayers);
      // NavMesh.SamplePosition(downHit2.point, out var navMeshHit2, 20f, NavMesh.AllAreas);
      // return navMeshHit2.position;
      Vector3 downHit2Point = downHit2.point;
      return downHit2Point;
    }
  }
}