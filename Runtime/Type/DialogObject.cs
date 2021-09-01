using PotatoTools.Dialog;
using System.Collections.Generic;
using UnityEngine;

namespace PotatoTools
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/DialogObject")]
    public class DialogObject : ScriptableObject
    {
        public List<CharacterObject> characters = new List<CharacterObject>();
        public List<DialogBlock> dialogs = new List<DialogBlock>();
        public bool looping = false;
    }
}