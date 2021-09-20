using UnityEngine;

namespace PotatoTools.Character
{
    public class CharacterAnimationController : MonoBehaviour
    {
        public SpriteRenderer spr;
        public Animator anim;

        public AudioSource step;

        private enum CharacterAnimEnum
        {
            IsWalk
        }

        public void SetWalk(bool walking, bool flip)
        {
            anim.SetBool(CharacterAnimEnum.IsWalk.ToString(), walking);
            spr.flipX = walking && flip;
        }

        public void PlayStep()
        {
            step.Play();
        }
    }
}