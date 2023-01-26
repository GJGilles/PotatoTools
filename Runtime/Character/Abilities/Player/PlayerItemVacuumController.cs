using PotatoTools.Inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PotatoTools.Character
{
    public class PlayerItemVacuumController : CharacterAbilityController
    {
        public ItemVacuumController vacuum;

        private void Start()
        {
            vacuum.inventory = PlayerService.GetInventory();
        }

        public override bool IsActive()
        {
            return false;
        }

        public override void Play(Animator anim)
        {
            throw new System.NotImplementedException();
        }
    }
}
