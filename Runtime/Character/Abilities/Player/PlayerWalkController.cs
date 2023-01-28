using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PotatoTools.Character
{
    [RequireComponent(typeof(PlayerController))]
    [RequireComponent(typeof(Collider2D))]
    public class PlayerWalkController : CharacterAbilityController
    {
        [SerializeField] private float speed = 0.5f;
        [SerializeField] private LayerMask groundMask;

        private bool isWalking = false;

        private PlayerController player;
        private Collider2D col;

        private void Start()
        {
            player = GetComponent<PlayerController>();
            col = GetComponent<Collider2D>();
        }

        public bool IsGrounded()
        {
            return Physics2D.BoxCast(col.bounds.center, (Vector2)col.bounds.size - new Vector2(0.02f, 0.02f), 0, Vector2.down, 0.02f, groundMask);
        }

        public override bool IsActive()
        {
            return IsGrounded();
        }

        public override void WhileActive()
        {
            Vector2 move = InputManager.GetMovement();
            int dir = Mathf.RoundToInt(move.x);
            player.MoveSmooth(dir * speed, null);
            isWalking = dir != 0;
        }

        public override void Play(Animator anim)
        {
            anim.SetBool("IsWalk", isWalking);
        }
    }
}
