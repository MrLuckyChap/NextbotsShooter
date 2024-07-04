using System;
using System.Linq;
using All_Imported_Assets.AMFPC.First_Person_Items___Arms.Scripts;
using All_Imported_Assets.AMFPC.Scriptables_Objects.Inventory.Scripts;
using All_Imported_Assets.AMFPC.Scriptables_Objects.Items.Equipements;
using All_Imported_Assets.AMFPC.Scriptables_Objects.Scripts;
using All_Imported_Assets.AMFPC.Scripts.Interfaces;
using CodeBase;
using UnityEngine;

namespace All_Imported_Assets.AMFPC.Interactables
{
    public class PickupItem : MonoBehaviour
    {
        public ItemObject item;
        public ItemObject Ammunition;
        public int pickAmount,totalAmount;
        public InventoryObject playerInventory;
        [Header("Settings for First Person Items")]
        [HideInInspector] public ItemManager ItemManager;
        public bool showItemOnPickup;
        [SerializeField] private TriggerObserver _triggerObserver;
        [SerializeField] private GameObject _parentObject;

        private void Awake()
        {
          _triggerObserver.TriggerEnter += TriggerEnter;
        }

        public void Interact()
        {
          // PickUpItem();
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
                  AddAmmo();
                  _parentObject.SetActive(false);
                  return;
                }
                if (!_hasItem && showItemOnPickup)
                {
                  InventorySlot weaponSlot = null;

                  foreach (InventorySlot slot in playerInventory.container)
                  {
                    WeaponObject weapon = slot.item as WeaponObject;
                    if (weapon != null)
                    {
                      weaponSlot = slot;
                      break;
                    }
                  }
                  ItemManager.SwitchToItem(item.FPItemIndex);
                  AddItemToInventory();

                  if (weaponSlot != null)
                  {
                    playerInventory.RemoveItem(weaponSlot);
                  }
                }
            }

            // AddItemToInventory();
        }

        private void AddAmmo()
        {
          if (totalAmount < pickAmount)
          {
            pickAmount = totalAmount;
          }
          totalAmount -= pickAmount;
          playerInventory.AddItem(Ammunition, pickAmount);
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
              _parentObject.SetActive(false);
            }
        }

        private void TriggerEnter(Collider obj)
        {
          PickUpItem();
        }

        public void OnDestroy()
        {
          foreach (InventorySlot slot in playerInventory.container)
          {
            WeaponObject weapon = slot.item as WeaponObject;
            if (weapon != null) playerInventory.RemoveItem(slot);
          }
        }
    }
}