using PotatoTools;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PotatoTools
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/CharacterObject", order = 7)]
    public class CharacterObject : ScriptableObject
    {
        public RuntimeAnimatorController animator;
        public Sprite sprite;

        [SerializeField]
        private List<DialogObject> dialogs = new List<DialogObject>();

        public List<DialogObject> GetDialogs() => dialogs;

        public void PushDialog(DialogObject d)
        {
            if (d.looping)
            {
                dialogs.RemoveAll(x => x.looping);
                dialogs.Insert(0, d);
            }
            else
            {
                dialogs.Add(d);
            }
        }

        public DialogObject PopDialog()
        {
            DialogObject d = dialogs.Last();
            if (!d.looping)
            {
                dialogs.RemoveAt(dialogs.Count - 1);
            }
            return d;
        }
    }
}