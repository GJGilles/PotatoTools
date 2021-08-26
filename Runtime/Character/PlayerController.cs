using PotatoTools.Game;
using PotatoTools.Inventory;
using PotatoTools.UI;
using UnityEngine;

namespace PotatoTools.Character
{
    public class PlayerController : CharacterMovementController
    {
        public PauseController pause;
        public ItemVacuumController vacuum;

        public float dropTime = 1f;

        private DropPlatformController platform;
        private SelectableController selection;

        private void Start()
        {
            vacuum.inventory = PlayerService.GetInventory();
        }

        protected override void Update()
        {
            base.Update();

            if (PlayerService.IsLocked()) return;

            Vector2 input = InputManager.GetMovement();
            SetMove(Mathf.RoundToInt(input.x));
            if (platform != null && input.y < 0)
            {
                platform.DropDown(col);
            }

            if (selection != null && InputManager.GetButtonTrigger(ButtonEnum.A))
            {
                selection.Select(this);
            }

            if (InputManager.GetButtonTrigger(ButtonEnum.B))
            {
                SetJump();
            }

            if (InputManager.GetButtonTrigger(ButtonEnum.Start))
            {
                Instantiate(pause);
            }
        }

        protected void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.TryGetComponent(out SelectableController s))
            {
                selection = s;
            }
            if (collision.gameObject.TryGetComponent(out DropPlatformController p))
            {
                platform = p;
            }
        }

        protected void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.TryGetComponent(out SelectableController s) && selection == s)
            {
                selection = null;
            }
            if (collision.gameObject.TryGetComponent(out DropPlatformController p) && platform == p)
            {
                platform = null;
            }
        }
    }
}