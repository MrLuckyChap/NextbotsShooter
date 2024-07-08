using All_Imported_Assets.AMFPC.Interactables;
using All_Imported_Assets.AMFPC.Scripts.Interfaces;
using All_Imported_Assets.AMFPC.UI.Scripts;
using UnityEngine;

namespace All_Imported_Assets.AMFPC.Camera.Scripts
{
    public class CameraRayCast : MonoBehaviour
    {
        [Range(0, 25)] public float interactDistance;
        [Range(0,1000)] public float rayRange;
        public LayerMask raycastIgnoredLayers;
        [HideInInspector] public bool damageableDetected, interactableDetected;
        [HideInInspector] public IDamageable damageable;
        [HideInInspector] public IInteractable interactbale;
        [HideInInspector] public UIManager UIReference;
        private bool _interactButtonEnabled = true;
        private UnityEngine.Camera _camera;
        private Vector3 _rayPosition;
        public RaycastHit hit;

        void Start()
        {
            _camera = UnityEngine.Camera.main;
            _rayPosition = new Vector3(0.5f, 0.5f, 0);
            GetComponent<CameraLook>().inputManager.onInteractInputDown.AddListener(InteractInputDown);
        }
        private void Update()
        {
            RaycastFront();
            SetInteractUI();
        }
        public void RaycastFront()
        {
            Ray ray = _camera.ViewportPointToRay(_rayPosition);
            if (Physics.Raycast(ray, out hit, rayRange, ~raycastIgnoredLayers))
            {
                float _distance = Vector3.Distance(hit.point, transform.position);
                if(_distance< interactDistance)
                {
                    interactbale = hit.transform.GetComponent<IInteractable>();
                }
                else
                {
                    interactbale = null;
                }
                if (interactbale != null)
                {
                    InteractableSettings _interactableSettings = hit.transform.GetComponent<InteractableSettings>();
                    UIReference.interctInfoText.text = _interactableSettings.interactInfo;
                }
                else
                {
                    UIReference.interctInfoText.text = "";
                }
                damageable = hit.transform.GetComponent<IDamageable>();

            }
            damageableDetected = damageable != null;
            interactableDetected = interactbale != null;
        }
        private void SetInteractUI()
        {
            if (interactableDetected && !_interactButtonEnabled)
            {
                UIReference.interactUI.SetActive(true);
                _interactButtonEnabled = true;
            }
            else if (_interactButtonEnabled && !interactableDetected)
            {
                UIReference.interactUI.SetActive(false);
                _interactButtonEnabled = false;
            }
        }
        private void InteractInputDown()
        {
            if(interactableDetected)
                interactbale.Interact();
        }
    }
}
