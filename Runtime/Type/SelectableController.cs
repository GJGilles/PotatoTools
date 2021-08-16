using PotatoTools.Character;
using UnityEngine;

namespace PotatoTools
{
    public abstract class SelectableController : MonoBehaviour
    {
        public abstract void Select(PlayerController player);
    }
}