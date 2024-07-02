using AMFPC.First_Person_Items___Arms.Scripts;
using AMFPC.Scriptables_Objects.Inventory.Scripts;
using AMFPC.Scriptables_Objects.Scripts;
using AMFPC.Scripts.Interfaces;
using UnityEngine;

namespace AMFPC.Interactables
{
    public class PickupItem : MonoBehaviour, IInteractable
    {
        public ItemObject item;
        public int pickAmount,totalAmount;
        public InventoryObject playerInventory;
        [Header("Settings for First Person Items")]
        [HideInInspector] public ItemManager ItemManager;
        public bool showItemOnPickup;
        private void Awake()
        {
            // ItemManager = GameObject.FindGameObjectWithTag("ItemManager").GetComponent<ItemManager>();
        }
        public void PickUpItem()
        {
            if (item.isFirstPersonItem) 
            {
                bool _hasItem = false;
                for (int n = 0; n < playerInventory.container.Count; n++)
                {
                    if (playerInventory.container[n].item == item)
                    {
                        _hasItem = true;
                    }
                }
                if (_hasItem)
                {
                    return;
                }
                if (!_hasItem && showItemOnPickup)
                {
                    ItemManager.SwitchToItem(item.FPItemIndex);
                }
            }

            AddItemToInventory();
        }
        private void AddItemToInventory()
        {
            if (totalAmount < pickAmount)
            {
                pickAmount = totalAmount;
            }
            totalAmount -= pickAmount;
            playerInventory.AddItem(item, pickAmount);
            if (totalAmount <= 0)
            {
                gameObject.SetActive(false);
            }
        }
        public void Interact()
        {
            PickUpItem();
        }
    }
}