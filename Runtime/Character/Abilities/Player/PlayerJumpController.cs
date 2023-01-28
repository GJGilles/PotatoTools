using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PotatoTools.Character
{
    [RequireComponent(typeof(PlayerController))]
    [RequireComponent(typeof(Collider2D))]
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

        private PlayerController player;
        private Collider2D col;

        private void Start()
        {
            player = GetComponent<PlayerController>();
            col = GetComponent<Collider2D>();
        }

        private void Update()
        {
            if (IsWall(Vector2.down)) SetGround();
            if (InputManager.GetButtonTrigger(ButtonEnum.B)) SetJumpInput();

            /* TODO: Make a seperate component for air control
            Vector2 move = InputManager.GetMovement();
            int dir = Mathf.RoundToInt(move.x);
            player.MoveSmooth(dir * speed, null);
            */

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
            return lastGround < timeGround && lastInput < timeInput && lastJumpCooldown > timeJumpCooldown;
        }

        public override void WhileActive()
        {
            player.MoveInstant(null, force);
            lastGround = timeGround;
            lastInput = timeInput;
            lastJumpCooldown = 0f;
        }

        public override void Play(Animator anim)
        {
        }
    }
}
