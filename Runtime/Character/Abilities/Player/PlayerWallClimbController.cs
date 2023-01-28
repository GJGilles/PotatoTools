using PotatoTools.Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PotatoTools.Character
{
    [RequireComponent(typeof(PlayerController))]
    [RequireComponent(typeof(Collider2D))]
    public class PlayerWallClimbController : CharacterAbilityController
    {
        [SerializeField] private float speed = 2f;
        [SerializeField] private float timeCancel = 0.2f;

        private ClimbingWallController climb;
        private float lastCancel = 0.3f;

        private PlayerController player;

        private void Start()
        {
            player = GetComponent<PlayerController>();
        }

        private void Update()
        {
            lastCancel += Time.deltaTime;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.TryGetComponent(out ClimbingWallController c))
            {
                climb = c;
                player.IgnoreGravity();
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.TryGetComponent(out ClimbingWallController c) && climb == c)
            {
                climb = null;
                player.ResetGravity();
            }
        }

        public override bool IsActive()
        {
            return climb != null && lastCancel > timeCancel;
        }

        public override void WhileActive()
        {
            if (InputManager.GetButtonHeld(ButtonEnum.B) > 0)
            {
                player.ResetGravity();
                lastCancel = 0;
            }
            else
            {
                player.IgnoreGravity();

                Vector2 move = InputManager.GetMovement();
                int x = Mathf.RoundToInt(move.x);
                int y = Mathf.RoundToInt(move.y);
                player.MoveSmooth(x * speed, y * speed);
            }
        }

        public override void Play(Animator anim)
        {
        }
    }
}
