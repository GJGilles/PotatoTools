using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PotatoTools.Character
{
    [RequireComponent(typeof(PlayerController))]
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(PlayerJumpController))]
    public class PlayerWallJumpController : CharacterAbilityController
    {
        [SerializeField] private float forceJump;
        [SerializeField] private float timeHang;
        [SerializeField] private float timeCooldown;
        [SerializeField] private float forceHang;

        private float lastHang = 0;
        private float lastCooldown = 0;

        private PlayerController player;
        private PlayerJumpController jump;

        private void Start()
        {
            player = GetComponent<PlayerController>();
            jump = GetComponent<PlayerJumpController>();
        }

        private void Update()
        {
            if (lastCooldown <= timeCooldown)
            {
                lastCooldown += Time.deltaTime;
                if (lastCooldown >= timeCooldown)
                {
                    lastHang = 0;
                }
            }
        }

        public override bool IsActive()
        {
            return (jump.IsWall(Vector2.left) || jump.IsWall(Vector2.right)) && !jump.IsWall(Vector2.down) && lastCooldown > timeCooldown;
        }

        public override void WhileActive()
        {
            if (lastHang > timeHang)
            {
                lastCooldown = 0;
                player.MoveInstant(null, null);
            }
            else if (InputManager.GetButtonHeld(ButtonEnum.B) > 0)
            {
                var dir = jump.IsWall(Vector2.left) ? 1 : -1;
                player.MoveInstant(dir * forceJump, null);
                jump.SetGround();
                jump.SetJumpInput();
                lastCooldown = 0;
                lastHang = 0;
            }
            else
            {
                player.MoveInstant(0, null);
                player.MoveSmooth(null, forceHang);
                lastHang += Time.deltaTime;
            }
        }

        public override void Play(Animator anim)
        {
        }
    }
}
