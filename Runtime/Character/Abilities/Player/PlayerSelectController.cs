using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PotatoTools.Character
{
    public class PlayerSelectController : CharacterAbilityController
    {
        private SelectableController selection;

        private void Update()
        {
            if (selection != null && InputManager.GetButtonTrigger(ButtonEnum.A))
            {
                selection.Select();
            }
        }

        protected virtual void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.TryGetComponent(out SelectableController s))
            {
                s.Highlight(true);
                selection = s;
            }
        }

        protected virtual void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.TryGetComponent(out SelectableController s) && selection == s)
            {
                s.Highlight(false);
                selection = null;
            }
        }

        public override bool IsActive()
        {
            return false;
        }

        public override void Play(Animator anim)
        {
            throw new System.NotImplementedException();
        }
    }
}
