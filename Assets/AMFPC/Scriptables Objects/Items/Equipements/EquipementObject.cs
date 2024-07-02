using System.Collections;
using System.Collections.Generic;
using AMFPC.Scriptables_Objects.Scripts;
using UnityEngine;

namespace AMFPC.Scriptables_Objects.Items.Equipements
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