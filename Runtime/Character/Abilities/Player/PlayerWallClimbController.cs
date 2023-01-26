using PotatoTools.Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PotatoTools.Character
{
    [RequireComponent(typeof(PlayerController))]
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerWallClimbController : CharacterAbilityController
    {
        [SerializeField] private float speed = 2f;
        [SerializeField] private float timeCancel = 0.2f;

        private ClimbingWallController climb;
        private float gravity;
        private float lastCancel = 0.3f;

        private Collider2D col;
        private Rigidbody2D rb;

        private void Start()
        {
            col = GetComponent<Collider2D>();
            rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            if (climb != null && lastCancel < timeCancel)
            {
                rb.gravityScale = gravity;
            }
            else if (climb != null)
            {
                rb.gravityScale = 0;

                Vector2 move = InputManager.GetMovement();
                int dir = Mathf.RoundToInt(move.y);
                rb.velocity = new Vector2(rb.velocity.x, dir * speed);
            }
        }

        private void OnTriggerEnter2D()
        {
            if (col.gameObject.TryGetComponent(out ClimbingWallController c))
            {
                climb = c;
                gravity = rb.gravityScale;
                rb.gravityScale = 0;
            }
        }

        private void OnTriggerExit2D()
        {
            if (col.gameObject.TryGetComponent(out ClimbingWallController c) && climb == c)
            {
                climb = null;
                rb.gravityScale = gravity;
            }
        }

        public override bool IsActive()
        {
            return climb != null && lastCancel > timeCancel;
        }

        public override void Play(Animator anim)
        {
            throw new System.NotImplementedException();
        }
    }
}
