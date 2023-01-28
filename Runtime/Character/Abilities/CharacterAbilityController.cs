using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PotatoTools.Character
{
    public abstract class CharacterAbilityController : MonoBehaviour
    {
        public RuntimeAnimatorController animator;

        public abstract bool IsActive();

        public abstract void WhileActive();

        public abstract void Play(Animator anim);
    }
}
