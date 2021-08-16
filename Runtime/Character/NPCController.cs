
namespace PotatoTools.Character
{
    public class NPCController : CharacterMovementController
    {
        public NPCSelectController selectable;
        public CharacterObject character;

        private void Start()
        {
            selectable.character = character;
            anim.anim.runtimeAnimatorController = character.animator;
        }
    }
}