using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PotatoTools.Inventory
{
    public static class ItemService
    {
        private static List<ItemObject> items = new List<ItemObject>();

        static ItemService()
        {
            items = AssetLoader.LoadObjects<ItemObject>();
        }

        public static ItemObject Get(int id)
        {
            return items.Find(x => x.GetHashCode() == id);
        }

        public static List<ItemObject> GetAll()
        {
            return items;
        }
    }
}
