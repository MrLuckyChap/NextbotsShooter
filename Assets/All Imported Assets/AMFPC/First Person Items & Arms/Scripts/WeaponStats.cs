using All_Imported_Assets.AMFPC.Scriptables_Objects.Inventory.Scripts;
using All_Imported_Assets.AMFPC.Scriptables_Objects.Items.Equipements;
using UnityEngine;

namespace All_Imported_Assets.AMFPC.First_Person_Items___Arms.Scripts
{
    public class WeaponStats : MonoBehaviour
    {
        [HideInInspector] public ItemSettings itemSettings;
        [HideInInspector] public int ammunitionCount;
        [HideInInspector] public float currentFireTimer, currentReloadTime;
        private int _ammoSlotIndex;
        [HideInInspector] public InventorySlot ammoSlot;
        [HideInInspector] public WeaponObject weaponItem;

        private void Awake()
        {
            itemSettings = GetComponent<ItemSettings>();
            weaponItem = itemSettings.item as WeaponObject;
            GetAmmoSlotIndex();
            ammoSlot = itemSettings.playerInventory.container[_ammoSlotIndex];
            currentReloadTime = weaponItem.reloadDuration;
        }

        private void Start()
        {
            ammunitionCount = weaponItem.maxMagazineSize;
        }

        public void SubstractFiredAmmo(int amount)
        {
            ammunitionCount -= amount;
        }

        public void RestoreMagazineCount()
        {
            int maxMagazineSize = weaponItem.maxMagazineSize;
            int ammoSlotAmount = ammoSlot.amount;
            int countForReload = maxMagazineSize - ammunitionCount;
            if (countForReload >= ammoSlotAmount)
            {
              ammunitionCount += ammoSlotAmount;
              ammoSlot.AddAmount(-ammoSlotAmount);
            }
            else if (countForReload < ammoSlotAmount)
            {
              ammunitionCount += countForReload;
              ammoSlot.AddAmount(-countForReload);
            }
        }

        private void GetAmmoSlotIndex()
        {
            for (int i = 0; i < itemSettings.playerInventory.container.Count; i++)
            {
                if (itemSettings.playerInventory.container[i].item == weaponItem.ammunitionType) _ammoSlotIndex = i;
            }
        }
    }
}
