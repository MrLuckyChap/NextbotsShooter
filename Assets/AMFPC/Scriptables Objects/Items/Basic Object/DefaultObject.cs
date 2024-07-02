using AMFPC.Scriptables_Objects.Scripts;
using UnityEngine;

namespace AMFPC.Scriptables_Objects.Items.Basic_Object
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
