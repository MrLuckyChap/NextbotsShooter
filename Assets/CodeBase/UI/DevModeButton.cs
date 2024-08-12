using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI
{
  public class DevModeButton : MonoBehaviour
  {
    [SerializeField] private GameObject _devModePopup;
    [SerializeField] private Button _devModeOpenButton;
    [SerializeField] private Button _devModeCloseButton;

    private float shakeThreshold = 1.3f;
    private float shakeDuration = 0.1f;
    private float currentShakeIntensity;
    private float shakeStartTime;
    private bool _isDevModePopupActive;

    private void Awake()
    {
      _devModeOpenButton.onClick.AddListener(() => _devModePopup.SetActive(true));
      _devModeCloseButton.onClick.AddListener(() => _devModePopup.SetActive(false));
    }

    void Update()
    {
      if (Input.acceleration.magnitude > shakeThreshold)
      {
        if (shakeStartTime == 0) shakeStartTime = Time.time;
        currentShakeIntensity = Mathf.Max(currentShakeIntensity, Input.acceleration.magnitude);
      }
      else
      {
        shakeStartTime = 0;
        currentShakeIntensity = 0;
      }

      if (shakeStartTime != 0 && Time.time - shakeStartTime > shakeDuration)
      {
        _devModePopup.SetActive(_isDevModePopupActive = !_isDevModePopupActive);
        shakeStartTime = 0;
      }
    }
  }
}