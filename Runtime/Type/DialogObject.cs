using PotatoTools.Dialog;
using System.Collections.Generic;
using UnityEngine;

namespace PotatoTools
{
    public abstract class DialogObject : ScriptableObject
    {
        public List<CharacterObject> characters = new List<CharacterObject>();
        public List<DialogBlock> dialogs = new List<DialogBlock>();
    }
}