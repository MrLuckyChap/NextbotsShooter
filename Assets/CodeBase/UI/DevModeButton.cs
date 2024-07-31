using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI
{
  public class DevModeButton : MonoBehaviour
  {
    [SerializeField] private GameObject _devModePopup;
    [SerializeField] private Button _devModeOpenButton;
    [SerializeField] private Button _devModeCloseButton;

    private void Awake()
    {
      _devModeOpenButton.onClick.AddListener(() => _devModePopup.SetActive(true));
      _devModeCloseButton.onClick.AddListener(() => _devModePopup.SetActive(false));
    }
  }
}