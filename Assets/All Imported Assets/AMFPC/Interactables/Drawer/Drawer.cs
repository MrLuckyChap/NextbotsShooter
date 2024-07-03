using All_Imported_Assets.AMFPC.Scripts.Interfaces;
using UnityEngine;

namespace All_Imported_Assets.AMFPC.Interactables.Drawer
{
    public class Drawer : MonoBehaviour,IInteractable
    {
        public Animator animator;
        private bool _drawerOpen;
        private void Start()
        {
            _drawerOpen = false;
        }
        public void Interact()
        {
            InteractDrawer();
        }
        private void InteractDrawer()
        {
            if (_drawerOpen)
            {
                animator.SetInteger("State", 2);
                _drawerOpen = false;
            }
            else
            {
                animator.SetInteger("State", 1);
                _drawerOpen = true;
            }

        }
    }
}
