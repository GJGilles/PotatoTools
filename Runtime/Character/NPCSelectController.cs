using PotatoTools.Dialog;
using System;
using System.Linq;
using UnityEngine;

namespace PotatoTools.Character
{
    public class NPCSelectController : SelectableController
    {
        [NonSerialized] public CharacterObject character;
        public DialogController dialog;

        public override void Select(PlayerController player)
        {
            var inst = Instantiate(dialog);
            inst.dialog = character.PopDialog();
            CharacterService.OnDialog.Invoke(character, inst.dialog);
        }
    }
}