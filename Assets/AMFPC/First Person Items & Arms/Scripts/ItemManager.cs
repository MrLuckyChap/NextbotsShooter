using System.Collections.Generic;
using AMFPC.Camera.Scripts;
using AMFPC.First_Person_Items___Arms.Headbob_Animations;
using AMFPC.Input.Scripts;
using AMFPC.Player_Controller.Scripts;
using AMFPC.Scriptables_Objects.Inventory.Scripts;
using AMFPC.UI.Scripts;
using UnityEngine;

namespace AMFPC.First_Person_Items___Arms.Scripts
{
    public class ItemManager : MonoBehaviour
    {
        [HideInInspector] public AimAssist aimAssist;
        [HideInInspector] public FieldOfView fieldOfView;
        [HideInInspector] public CameraLook cameraLook;
        [HideInInspector] public FirstPersonItemHeadbob firstPersonItemHeadbob;
        [HideInInspector] public CameraRayCast cameraRayCast;
        [HideInInspector] public InputManager inputManager;
        [HideInInspector] public UIManager UIReference;
        public UnityEngine.Camera secondaryCamera;
        [HideInInspector] public PlayerController playerController;
        public InventoryObject playerInventory;
        public AudioSource audioSource;
        public AnimationCurve itemSwitchAnimation;
        [Range(0, 1)] public float switchThreshhold;
        private float _switchRot;
        [HideInInspector] public bool switching;
        private float _switchTimer,_lerpTime;
        [HideInInspector] public List<GameObject> items;
        private int _switchIndex, _currentItemIndex = 9999;
        private bool _switched;
        private void Awake()
        {
            // playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
            // UIReference = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
            GameObject _mainCamera = UnityEngine.Camera.main.gameObject;
            aimAssist = _mainCamera.GetComponent<AimAssist>();
            fieldOfView = _mainCamera.GetComponent<FieldOfView>();
            cameraLook = _mainCamera.GetComponent<CameraLook>();
            cameraRayCast = _mainCamera.GetComponent<CameraRayCast>();
            firstPersonItemHeadbob = transform.parent.GetComponent<FirstPersonItemHeadbob>();
        }

        void Start()
        {
            inputManager = playerController.inputManager;
            GetItems();
            _lerpTime = itemSwitchAnimation.keys[itemSwitchAnimation.length - 1].time;
        }
        void Update()
        {
            ManageSwitching();
        }
        public void GetItems()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                GameObject _item = transform.GetChild(i).gameObject;
                items.Add(_item);
            }
        }
        public void ActivateItem(int index)
        {
            for (int i = 0; i < items.Count; i++)
            {
                if(i == index)
                {
                    items[i].SetActive(true);
                    _currentItemIndex = i;
                }
                else
                {
                    items[i].SetActive(false);
                }
            }
        }
        public void SwitchToItem(int index)
        {
            if (index == _currentItemIndex)
            { 
                return; 
            }
            _switchTimer = 0;
            _switched = false;
            _switchIndex = index;
            switching = true;
        }
        private void ManageSwitching()
        {
            if (switching)
            {
                _switchTimer += Time.deltaTime;
                if (_switchTimer > _lerpTime * switchThreshhold && !_switched)
                {
                    ActivateItem(_switchIndex);
                    _switched = true;
                }
                if (_switchTimer > _lerpTime)
                {
                    _switchTimer = 0;
                    switching = false;
                }

                _switchRot = itemSwitchAnimation.Evaluate(_switchTimer) * 45;
            }
            transform.localRotation = Quaternion.Euler(_switchRot, 0, 0);
        }
    }
}
