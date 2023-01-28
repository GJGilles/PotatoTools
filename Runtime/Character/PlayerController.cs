using PotatoTools.Game;
using PotatoTools.Inventory;
using PotatoTools.UI;
using UnityEngine;

namespace PotatoTools.Character
{

    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController : ActorController
    {
        [SerializeField] private float timeSmooth = 1f;
        [SerializeField] private PauseController pause;

        private Vector2 velocitySmooth;
        private float lastSmooth;

        private float gravityScale;

        private Rigidbody2D rb;

        protected override void Start()
        {
            base.Start();

            rb = GetComponent<Rigidbody2D>();

            gravityScale = rb.gravityScale;
        }

        protected override void Update()
        {
            base.Update();

            if (PlayerService.IsLocked()) return;

            if (InputManager.GetButtonTrigger(ButtonEnum.Start))
            {
                Instantiate(pause);
            }

            if (lastSmooth < timeSmooth)
            {
                if (lastSmooth + Time.deltaTime <= timeSmooth)
                {
                    rb.velocity += velocitySmooth * (Time.deltaTime / timeSmooth);
                }
                else
                {
                    rb.velocity += velocitySmooth * ((timeSmooth - lastSmooth) / timeSmooth);
                }
                lastSmooth += Time.deltaTime;
            }
        }

        public void MoveInstant(float? x, float? y)
        {
            rb.velocity = new Vector2(x ?? rb.velocity.x, y ?? rb.velocity.y);
            lastSmooth = timeSmooth;
        }

        public void MoveSmooth(float? x, float? y, bool overwrite = false)
        {
            if (overwrite || lastSmooth >= timeSmooth)
            {
                lastSmooth = 0f;
                velocitySmooth = new Vector2(x ?? rb.velocity.x, y ?? rb.velocity.y) - rb.velocity;
            }
        }

        public void IgnoreGravity()
        {
            rb.gravityScale = 0;
        }

        public void ResetGravity()
        {
            rb.gravityScale = gravityScale;
        }
    }
}