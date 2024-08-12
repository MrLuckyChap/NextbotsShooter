using All_Imported_Assets.AMFPC.Scripts;
using CodeBase;
using UnityEngine;

namespace All_Imported_Assets.AMFPC.Player_Controller.Scripts
{
  public class PlayerRespawn : MonoBehaviour
  {
    public AISpawner AISpawner { get; set; }

    private CharacterController _characterController;
    private HealthManager healthManager;
    private PlayerController _playerController;

    private void Awake()
    {
      _playerController = GetComponent<PlayerController>();
      _characterController = _playerController.characterController;
      healthManager = GetComponent<HealthManager>();
    }

    public void RespawnPlayer()
    {
      _characterController.enabled = false;
#if !UNITY_EDITOR
      Vector3 spawnPoint = AISpawner.GetNavMeshRandomPoint();
      spawnPoint.y += 1f;
      _playerController.transform.position = spawnPoint;
#endif
      _characterController.enabled = true;
      healthManager.RestoreHealth();
      _playerController.defaultMovement.mechanicEnabled = true;
      _playerController.jumping.mechanicEnabled = true;
      _playerController.crouching.mechanicEnabled = true;
    }
  }
}