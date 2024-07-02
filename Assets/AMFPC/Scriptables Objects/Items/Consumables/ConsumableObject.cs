using AMFPC.Scriptables_Objects.Scripts;
using UnityEngine;

namespace AMFPC.Scriptables_Objects.Items.Consumables
{
    [CreateAssetMenu(fileName = "New Consumable Object", menuName = "Inventory System/Items/Consumable")]
    public class ConsumableObject : ItemObject
    {
        [Header("Consumable Item Settings")]
        public int restoreHealthValue;
        private void Awake()
        {
            type = ItemType.consumable;
        }
    }
}
