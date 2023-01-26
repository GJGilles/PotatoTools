using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PotatoTools.Character
{
    [RequireComponent(typeof(PlayerController))]
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerJumpController : CharacterAbilityController
    {
        [SerializeField] private float force = 5f;
        [SerializeField] private LayerMask groundMask;

        [SerializeField] [Tooltip("How long we allow jumping after leaving the ground")] private float timeGround = 0.2f;
        [SerializeField] [Tooltip("How long we disallow jumping after jumping")] private float timeJumpCooldown = 0.3f;
        [SerializeField] [Tooltip("How long we allow jumping after requesting a jump")] private float timeInput = 0.2f;

        private float lastGround = 0.3f;
        private float lastJumpCooldown = 0.3f;
        private float lastInput = 0.3f;

        private Collider2D col;
        private Rigidbody2D rb;

        private void Start()
        {
            col = GetComponent<Collider2D>();
            rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            if (InputManager.GetButtonTrigger(ButtonEnum.B)) SetJumpInput();

            if (IsWall(Vector2.down)) SetGround();

            if (lastGround < timeGround && lastInput < timeInput && lastJumpCooldown > timeJumpCooldown)
            {
                rb.velocity = new Vector2(rb.velocity.x, force);
                lastGround = timeGround;
                lastInput = timeInput;
                lastJumpCooldown = 0f;
            }

            lastGround += Time.deltaTime;
            lastInput += Time.deltaTime;
            lastJumpCooldown += Time.deltaTime;
        }

        public bool IsWall(Vector2 direction)
        {
            return Physics2D.BoxCast(col.bounds.center, (Vector2)col.bounds.size - new Vector2(0.02f, 0.02f), 0, direction, 0.02f, groundMask);
        }


        public void SetGround()
        {
            lastGround = 0f;
        }

        public void SetJumpInput()
        {
            lastInput = 0f;
        }

        public override bool IsActive()
        {
            return !IsWall(Vector2.down);
        }

        public override void Play(Animator anim)
        {
            throw new System.NotImplementedException();
        }
    }
}
