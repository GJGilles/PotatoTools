using System.Collections;
using UnityEngine;

namespace PotatoTools
{
    public class ItemStack
    {
        public ItemObject item;
        public int number;

        public bool permanent = false;

        public ItemStack(ItemObject i, int n)
        {
            item = i;
            number = n;
        }
    }
}