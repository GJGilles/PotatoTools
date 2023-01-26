using PotatoTools.Game;
using System;
using System.Collections.Generic;
using UnityEngine;

// TODO: Move abilities to seperate components and this will be the central controller / animation player
namespace PotatoTools.Character
{
    [RequireComponent(typeof(Animator))]
    public class CharacterController : MonoBehaviour
    {
        private Animator animator;

        protected virtual void Start()
        {
            animator = GetComponent<Animator>();
        }

        protected virtual void Update()
        {
            var abilities = new List<CharacterAbilityController>(GetComponents<CharacterAbilityController>()); //Need to reverse?
            foreach (var a in abilities)
            {
                if (a.IsActive())
                {
                    animator.runtimeAnimatorController = a.animator;
                    a.Play(animator);
                    break;
                }
            }
        }
    }
}