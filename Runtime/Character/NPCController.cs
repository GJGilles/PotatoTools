
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

            CharacterService.SetPosition(character, transform.position);
            CharacterService.OnMove.AddListener(Move);
        }

        private void OnDestroy()
        {
            CharacterService.OnMove.RemoveListener(Move);
        }

        private void Move(CharacterObject c)
        {
            if (character == c)
            {
                transform.position = CharacterService.GetPosition(c);
            }
        }
    }
}