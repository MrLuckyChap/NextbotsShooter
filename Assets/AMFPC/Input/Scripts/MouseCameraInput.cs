using UnityEngine;

namespace AMFPC.Input.Scripts
{
    public class MouseCameraInput : MonoBehaviour
    {
        private InputManager _inputManager;
        private bool _cursorLocked;
        public KeyCode fire,ADS,inventory;
        private void Start()
        {
            _cursorLocked = false;
            ToggleLockCursor();
        }
        private void Awake()
        {
            _inputManager = transform.parent.GetComponent<InputManager>();
            _inputManager.toggleInventoryUI.AddListener(ToggleLockCursor);
        }
        void Update()
        {
            // Camera input
            _inputManager.cameraInput.x = UnityEngine.Input.GetAxisRaw("Mouse Y");
            _inputManager.cameraInput.y = UnityEngine.Input.GetAxisRaw("Mouse X");
            // Aim Down Sight
            if (UnityEngine.Input.GetKeyDown(ADS))
            {
                _inputManager.ADSInputDown();
            }
            if (UnityEngine.Input.GetKeyUp(ADS))
            {
                _inputManager.ADSInputUp();
            }
            // Firing 
            if (UnityEngine.Input.GetKeyDown(fire))
            {
                _inputManager.FireInputDown();
            }
            if (UnityEngine.Input.GetKeyUp(fire))
            {
                _inputManager.FireInputUp();
            }
            // Inventory UI
            if (UnityEngine.Input.GetKeyDown(inventory))
            {
                _inputManager.ToggleInventoryUI();
            }
        }
        public void ToggleLockCursor()
        {
            _cursorLocked = !_cursorLocked;
            if (_cursorLocked)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        
        }
    }
}
