using AMFPC.Input.Joystick;
using AMFPC.Player_Inventory;
using UnityEngine;
using UnityEngine.UI;

namespace AMFPC.UI.Scripts
{
    public class UIManager : MonoBehaviour
    {
        public GameObject crosshair;
        public FPItemInfoUI FPItemInfoUI;
        public Joystick joystick;
        public InventoryDisplay inventoryDisplay;
        public GameObject interactUI;
        public Text interctInfoText;
    }
}
