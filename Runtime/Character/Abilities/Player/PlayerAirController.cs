using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PotatoTools.Character
{
    [RequireComponent(typeof(PlayerController))]
    public class PlayerAirController : CharacterAbilityController
    {
        [SerializeField] private float speed = 3f;

        private PlayerController player;

        private void Start()
        {
            player = GetComponent<PlayerController>();
        }

        public override bool IsActive()
        {
            return true;
        }

        public override void WhileActive()
        {
            var move = InputManager.GetMovement();
            int dir = Mathf.RoundToInt(move.x);
            player.MoveSmooth(dir * speed, null);
        }

        public override void Play(Animator anim)
        {
        }
    }
}
