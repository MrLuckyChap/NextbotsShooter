using UnityEngine;
using UnityEngine.EventSystems;

namespace AMFPC.Input.Scripts
{
    public class UnityEditorScreenSwipeInput : MonoBehaviour
    {
        private InputManager _inputManager;
        private Vector3 _lastPos, _deltaPos;
        [Range(0, 2)] public float sensitivity;
        private bool _mouseOverUI;
        private void Awake()
        {
            _inputManager = transform.parent.GetComponent<InputManager>();
        }
        void Update()
        {
#if (UNITY_EDITOR)
            if (UnityEngine.Input.GetMouseButtonDown(0))
            {
                if (EventSystem.current.IsPointerOverGameObject() == true)
                {
                    _mouseOverUI = true;
                }
                _lastPos = UnityEngine.Input.mousePosition;
            }
            if (UnityEngine.Input.GetMouseButtonUp(0))
            {
                _mouseOverUI = false;
            }

            if (!_mouseOverUI)
            {
                if (UnityEngine.Input.GetMouseButton(0))
                {
                    _deltaPos = _lastPos - UnityEngine.Input.mousePosition;
                    _inputManager.cameraInput.y = Mathf.Lerp(_inputManager.cameraInput.y, -_deltaPos.x * sensitivity,15*Time.deltaTime) ;
                    _inputManager.cameraInput.x = Mathf.Lerp(_inputManager.cameraInput.x, -_deltaPos.y * sensitivity, 15 * Time.deltaTime);
                }
                else
                {
                    _inputManager.cameraInput = Vector2.zero;
                }
                _lastPos = UnityEngine.Input.mousePosition;
            }
#endif

        }
    }
}
