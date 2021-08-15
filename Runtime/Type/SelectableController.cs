using Assets.Scripts.Controllers.Character;
using System.Collections;
using UnityEngine;

namespace PotatoTools
{
    public abstract class SelectableController : MonoBehaviour
    {
        public abstract void Select(PlayerController player);
    }
}