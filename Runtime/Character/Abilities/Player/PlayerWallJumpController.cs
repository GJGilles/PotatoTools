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

        private float lastHang;
        private float lastCooldown;

        private Rigidbody2D rb;
        private PlayerJumpController jump;

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            jump = GetComponent<PlayerJumpController>();
        }

        private void Update()
        {
            if (lastCooldown < timeCooldown)
            {
                lastCooldown += Time.deltaTime;
            }
            else if ((jump.IsWall(Vector2.left) || jump.IsWall(Vector2.right)) && !jump.IsWall(Vector2.down))
            {
                if (lastHang > timeHang)
                {
                    lastCooldown = 0;
                    lastHang = 0;
                }
                else if (InputManager.GetButtonTrigger(ButtonEnum.B))
                {
                    var dir = jump.IsWall(Vector2.left) ? 1 : -1;
                    rb.velocity = new Vector2(dir * forceJump, 0);
                    jump.SetJumpInput();
                }
                else 
                {
                    rb.velocity += Vector2.up * forceHang;
                    lastHang += Time.deltaTime;
                }
            }
        }

        public override bool IsActive()
        {
            throw new System.NotImplementedException();
        }

        public override void Play(Animator anim)
        {
            throw new System.NotImplementedException();
        }
    }
}
