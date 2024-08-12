using UnityEngine;

namespace CodeBase.Infrastructure.StaticData
{
  [CreateAssetMenu(fileName = "AllLevelsData", menuName = "Data/AllLevels")]
  public class AllLevelsData : ScriptableObject
  {
    public GameObject PlayerController;
    public GameObject PlayerCamera;
    public GameObject InputManager;
    public GameObject GameHud;
    public GameObject AK47;
    public GameObject Shotgun;
    public GameObject Pistol;
    
  }
}