using UnityEngine;

namespace AMFPC.UI.Health_Bar.Billboard_Health_Bar
{
    public class BillBoard : MonoBehaviour
    {
        private Transform _cameraTransform;
        private void Awake()
        {
            _cameraTransform = UnityEngine.Camera.main.transform;
        }
        void LateUpdate()
        {
            transform.LookAt(transform.position + _cameraTransform.forward);
        }
    }
}
