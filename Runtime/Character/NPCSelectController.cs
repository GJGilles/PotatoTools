using PotatoTools.Dialog;
using System;
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
            inst.dialog = character.dialogs.Pop();
        }
    }
}