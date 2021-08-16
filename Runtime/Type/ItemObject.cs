using System.Collections;
using UnityEngine;

namespace PotatoTools
{
    public abstract class ItemObject : ScriptableObject
    {
        public string title;
        public Sprite spr;
        public int stack = 10;
    }
}