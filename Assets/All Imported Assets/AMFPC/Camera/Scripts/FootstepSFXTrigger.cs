using All_Imported_Assets.AMFPC.Scripts;
using UnityEngine;

namespace All_Imported_Assets.AMFPC.Camera.Scripts
{
    public class FootstepSFXTrigger : MonoBehaviour
    {
        private SFXManager _SFXManager;
        private void Awake()
        {
            _SFXManager = GameObject.FindGameObjectWithTag("Player").GetComponent<SFXManager>();
        }
        public void PlayWalkSFX()
        {
            _SFXManager.PlayWalkSFX();
        }
        public void PlayRunSFX()
        {
            _SFXManager.PlayRunSFX();
        }
    }
}
