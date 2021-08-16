using PotatoTools.Dialog;
using System.Collections.Generic;
using UnityEngine;

namespace PotatoTools
{
    public abstract class DialogObject : ScriptableObject
    {
        public List<Sprite> characters = new List<Sprite>();
        public List<DialogBlock> dialogs = new List<DialogBlock>();
    }
}