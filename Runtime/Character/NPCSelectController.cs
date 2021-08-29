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
            CharacterService.OnDialog.Invoke(character);

            var inst = Instantiate(dialog);
            inst.dialog = character.dialogs.Last();
            character.dialogs.Remove(character.dialogs.Last());
        }
    }
}