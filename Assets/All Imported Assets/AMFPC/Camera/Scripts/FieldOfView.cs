using UnityEngine;

namespace All_Imported_Assets.AMFPC.Camera.Scripts
{
    public class FieldOfView : MonoBehaviour
    {
        private UnityEngine.Camera _mainCamera;
        [HideInInspector] public float targetFOV,initialFOV;
        private void Awake()
        {
            _mainCamera = UnityEngine.Camera.main;
        }
        private void Start()
        {
            targetFOV = initialFOV = _mainCamera.fieldOfView;
        }
        private void Update()
        {
            ManageFOV();
        }
        private void ManageFOV()
        {
            _mainCamera.fieldOfView = Mathf.Lerp(_mainCamera.fieldOfView, targetFOV, 20 * Time.deltaTime);
        }
    }
}
