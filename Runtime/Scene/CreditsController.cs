using System.Collections;
using UnityEngine;

namespace PotatoTools.Scene
{
    public class CreditsController : MonoBehaviour
    {
        void Update()
        {
            if (InputManager.GetButtonTrigger(ButtonEnum.A))
            {
                SceneService.SetNext(SceneEnum.Title);
            }
        }
    }
}