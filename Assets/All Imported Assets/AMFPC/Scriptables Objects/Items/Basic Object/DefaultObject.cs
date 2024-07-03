using All_Imported_Assets.AMFPC.Scriptables_Objects.Scripts;
using UnityEngine;

namespace All_Imported_Assets.AMFPC.Scriptables_Objects.Items.Basic_Object
{
    [CreateAssetMenu(fileName = "New Default Object",menuName = "Inventory System/Items/Default")]
    public class DefaultObject : ItemObject
    {
        public void Awake()
        {
            type = ItemType.basic;
        }
    }
}
