using PotatoTools.Character;
using UnityEngine;

namespace PotatoTools
{
    [RequireComponent(typeof(Collider2D))]
    public abstract class SelectableController : MonoBehaviour
    {
        public abstract void Select(PlayerController player);
    }
}