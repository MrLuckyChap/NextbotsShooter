using All_Imported_Assets.AMFPC.Scriptables_Objects.Scripts;
using UnityEngine;

namespace All_Imported_Assets.AMFPC.Scriptables_Objects.Items.Equipements
{
    public class EquipementObject : ItemObject
    {
        [Header("Equipement Settings")]
        public EquipementType equipementType;
    }
    public enum EquipementType
    {
        weapon,
        armor,
        basic,
    }
}