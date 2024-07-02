using UnityEngine;

namespace AMFPC.Input.Scripts
{
    public class KeyboardInput : MonoBehaviour
    {
        private InputManager _inputManager;
        private Vector2 _moveInput;
        public KeyCode jump, crouch, reload, interact;
        private void Awake()
        {
            _inputManager = transform.parent.GetComponent<InputManager>();
        }
        void Update()
        {
       
            _moveInput.x = UnityEngine.Input.GetAxisRaw("Horizontal");
            _moveInput.y = UnityEngine.Input.GetAxisRaw("Vertical");

            _inputManager.moveInput = _moveInput;
            // Jumping
            if (UnityEngine.Input.GetKeyDown(jump))
            {
                _inputManager.JumpInputDown();
            }
            if (UnityEngine.Input.GetKeyUp(jump))
            {
                _inputManager.JumpInputUp();
            }
            // Crouching
            if (UnityEngine.Input.GetKeyDown(crouch))
            {
                _inputManager.CrouchInputDown();
            }
            if (UnityEngine.Input.GetKeyUp(crouch))
            {
                _inputManager.CrouchInputUp();
            }
            //Reloading 
            if (UnityEngine.Input.GetKeyDown(reload))
            {
                _inputManager.ReloadInputDown();
            }
            if (UnityEngine.Input.GetKeyUp(reload))
            {
                _inputManager.ReloadInputUp();
            }
            // Interacting
            if (UnityEngine.Input.GetKeyDown(interact))
            {
                _inputManager.InteractInputDown();
            }
            if (UnityEngine.Input.GetKeyUp(interact))
            {
                _inputManager.InteracInputUp();
            }
        }
    }
}
