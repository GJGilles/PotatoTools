using PotatoTools;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PotatoTools
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/CharacterObject", order = 7)]
    public class CharacterObject : ScriptableObject
    {
        public RuntimeAnimatorController animator;
        public Sprite sprite;
    }
}