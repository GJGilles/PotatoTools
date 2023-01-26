using PotatoTools.Game;
using PotatoTools.Inventory;
using PotatoTools.UI;
using UnityEngine;

namespace PotatoTools.Character
{

    //TODO: Move item vacuum and selection into ability? components
    public class PlayerController : CharacterController
    {
        public PauseController pause;

        protected override void Update()
        {
            base.Update();

            if (PlayerService.IsLocked()) return;

            if (InputManager.GetButtonTrigger(ButtonEnum.Start))
            {
                Instantiate(pause);
            }
        }
    }
}