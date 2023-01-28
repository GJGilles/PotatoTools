using PotatoTools.Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PotatoTools.Character
{
    [RequireComponent(typeof(PlayerController))]
    [RequireComponent(typeof(Collider2D))]
    public class PlayerDropPlatformController : CharacterAbilityController
    {
        [SerializeField] private float timeInput = 0.2f;

        private DropPlatformController platform;
        private float lastInput = 0.3f;

        private Collider2D col;

        private void Start()
        {
            col = GetComponent<Collider2D>();
        }

        public void Update()
        {
            Vector2 move = InputManager.GetMovement();
            int dir = Mathf.RoundToInt(move.y);
            if (dir < 0)
                lastInput = 0f;

            if (platform != null && lastInput < timeInput)
            {
                platform.DropDown(col);
            }

            lastInput += Time.deltaTime;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {

            if (collision.gameObject.TryGetComponent(out DropPlatformController p))
            {
                platform = p;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {

            if (collision.gameObject.TryGetComponent(out DropPlatformController p) && platform == p)
            {
                platform = null;
            }
        }

        public override bool IsActive()
        {
            return platform != null && lastInput < timeInput;
        }

        public override void WhileActive()
        {

        }

        public override void Play(Animator anim)
        {
            throw new System.NotImplementedException();
        }
    }
}
