using All_Imported_Assets.AMFPC.Input.Scripts;
using All_Imported_Assets.AMFPC.Player_Controller.Scripts;
using UnityEngine;

namespace All_Imported_Assets.AMFPC.Camera.Scripts
{
    public class CameraLook : MonoBehaviour
    {
        private Transform _playerTransform;
        [HideInInspector] public PlayerController PlayerController;
        [HideInInspector] public InputManager inputManager;
        [Range(0,100)] public float sensitivity;
        [HideInInspector] public float sensitivityMultiplier;
        private float _xRotation;
        [HideInInspector] public Vector2 rot, additionalRot;
        [HideInInspector] public bool canRotateCamera;
        private Transform _cameraPositionReference;
        
        void Start() 
        {
            _playerTransform = PlayerController.transform;
            _cameraPositionReference = _playerTransform.GetComponent<SetCameraReferencePosition>().cameraReference;
            inputManager = PlayerController.inputManager;
            _playerTransform.forward = transform.parent.transform.forward;

            sensitivityMultiplier = 1;
            canRotateCamera = true;
        }
        void Update() 
        {
            SetRotationValues();
        }
        private void LateUpdate()
        {
            RotateCamera();
            SetPosition(_cameraPositionReference.position);
        }
        private void SetRotationValues()
        {
            if (!canRotateCamera) return;
            Vector3 _input = inputManager.cameraInput;
            rot.x -= _input.x * (sensitivity* sensitivityMultiplier) * Time.fixedDeltaTime   ;
            rot.x = Mathf.Clamp(rot.x+ additionalRot.x, -90, 90);
            rot.y = _input.y * (sensitivity * sensitivityMultiplier) + additionalRot.y;
            _xRotation = transform.rotation.x + rot.x;
        }
        private void RotateCamera()
        {
            if( canRotateCamera)
            {
                transform.localRotation = Quaternion.Euler(_xRotation, 0, 0);
                _playerTransform.Rotate(_playerTransform.up, rot.y*Time.fixedDeltaTime);
                transform.parent.parent.Rotate(_playerTransform.up, rot.y * Time.fixedDeltaTime);
            }
        }
        private void SetPosition(Vector3 position)
        {
            transform.parent.parent.position = position;
        }

    }
}