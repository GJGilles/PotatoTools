using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PotatoTools.Character
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(PlayerController))]
    public class NewBehaviourScript : CharacterAbilityController
    {
        [SerializeField] private float speed = 0.5f;

        private bool isWalking = false;

        private Rigidbody2D rb;

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            Vector2 move = InputManager.GetMovement();
            int dir = Mathf.RoundToInt(move.x);
            rb.velocity = new Vector2(dir * speed, rb.velocity.y);
        }

        public override bool IsActive()
        {
            return true;
        }

        public override void Play(Animator anim)
        {
            anim.SetBool("walking", isWalking);
        }
    }
}
