using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PotatoTools
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/GamepadObject")]
    public class GamepadObject : ScriptableObject
    {
        public GamepadEnum gamepad = GamepadEnum.PS4;
        public List<ButtonData> buttons = new List<ButtonData>();

        [Serializable]
        public class ButtonData
        {
            public ButtonEnum button = ButtonEnum.A;
            public Sprite spr;
        }
    }
}
