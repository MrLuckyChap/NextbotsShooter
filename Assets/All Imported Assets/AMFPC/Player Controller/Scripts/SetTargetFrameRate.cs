using UnityEngine;

namespace All_Imported_Assets.AMFPC.Player_Controller.Scripts
{
    public class SetTargetFrameRate : MonoBehaviour
    {
        public int targetFrameRate;
        private void Start()
        { 
            Application.targetFrameRate = Mathf.Abs(targetFrameRate); 
        }
    }
}
