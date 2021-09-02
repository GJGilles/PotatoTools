using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.DualShock;
using UnityEngine.InputSystem.Switch;
using UnityEngine.InputSystem.XInput;

namespace PotatoTools
{
    public enum GamepadEnum
    {
        PS4,
        Xbox,
        Switch,
        None
    }

    public enum ButtonEnum
    {
        A,
        B,
        X,
        Y,
        Start,
        L1,
        L2,
        R1,
        R2,
    }

    public static class InputManager
    {
        private static Dictionary<GamepadEnum, Dictionary<ButtonEnum, Sprite>> gamepads = new Dictionary<GamepadEnum, Dictionary<ButtonEnum, Sprite>>();

        static InputManager()
        {
            var objs = AssetLoader.LoadObjects<GamepadObject>();
            foreach (var g in objs)
            {
                gamepads[g.gamepad] = g.buttons.ToDictionary(x => x.button, x => x.spr);
            }
        }

        private static float ReadInput(List<ButtonControl> inputs)
        {
            foreach (var input in inputs)
            {
                if (input == null) continue;

                float val = input.ReadValue();
                if (val != 0)
                {
                    return val;
                }
            }

            return 0f;
        }

        private static bool ReadTrigger(List<ButtonControl> inputs)
        {
            foreach (var input in inputs)
            {
                if (input == null) continue;

                bool val = input.wasPressedThisFrame;
                if (val)
                {
                    return val;
                }
            }

            return false;
        }

        private static List<ButtonControl> GetButtons(ButtonEnum id)
        {
            switch (id)
            {
                default:
                case ButtonEnum.A:
                    return new List<ButtonControl>() { 
                        Gamepad.current?.buttonSouth,
                        Keyboard.current?.zKey
                    };
                case ButtonEnum.B:
                    return new List<ButtonControl>() {
                        Gamepad.current?.buttonEast,
                        Keyboard.current?.xKey,
                        Keyboard.current?.spaceKey
                    };
                case ButtonEnum.X:
                    return new List<ButtonControl>() {
                        Gamepad.current?.buttonWest,
                        Keyboard.current?.eKey
                    };
                case ButtonEnum.Y:
                    return new List<ButtonControl>() {
                        Gamepad.current?.buttonNorth,
                        Keyboard.current?.qKey
                    };
                case ButtonEnum.Start:
                    return new List<ButtonControl>() { 
                        Gamepad.current?.startButton,
                        Keyboard.current?.escapeKey
                    };
                case ButtonEnum.L1:
                    return new List<ButtonControl>() {
                        Gamepad.current?.leftShoulder,
                        Keyboard.current?.digit1Key
                    };
                case ButtonEnum.L2:
                    return new List<ButtonControl>() {
                        Gamepad.current?.leftTrigger,
                        Keyboard.current?.digit2Key
                    };
                case ButtonEnum.R1:
                    return new List<ButtonControl>() {
                        Gamepad.current?.rightShoulder,
                        Keyboard.current?.digit3Key
                    };
                case ButtonEnum.R2:
                    return new List<ButtonControl>() {
                        Gamepad.current?.rightTrigger,
                        Keyboard.current?.digit4Key
                    };

            }
        }

        public static GamepadEnum GetGamepadType()
        {
            if (Gamepad.current is DualShockGamepad)
            {
                return GamepadEnum.PS4;
            }
            else if (Gamepad.current is XInputController)
            {
                return GamepadEnum.Xbox;
            }
            else if (Gamepad.current is SwitchProControllerHID)
            {
                return GamepadEnum.Switch;
            }
            else
            {
                return GamepadEnum.None;
            }
        }

        public static Sprite GetSprite(ButtonEnum button)
        {
            return gamepads[GetGamepadType()][button];
        }

        public static float GetHorzAxis()
        {
            float left = ReadInput(new List<ButtonControl>() {
                Gamepad.current?.leftStick?.left,
                Keyboard.current?.leftArrowKey
            });

            float right = ReadInput(new List<ButtonControl>() { 
                Gamepad.current?.leftStick?.right,
                Keyboard.current?.rightArrowKey
            });

            return right - left;
        }

        public static float GetVertAxis()
        {
            float up = ReadInput(new List<ButtonControl>() {
                Gamepad.current?.leftStick?.up,
                Keyboard.current?.upArrowKey
            });

            float down = ReadInput(new List<ButtonControl>()
            {
                Gamepad.current?.leftStick?.down,
                Keyboard.current?.downArrowKey
            });

            return up - down;
        }

        public static Vector2 GetMovement()
        {
            return new Vector2(GetHorzAxis(), GetVertAxis());
        }

        public static float GetButtonHeld(ButtonEnum button)
        {
            return ReadInput(GetButtons(button));
        }

        public static bool GetButtonTrigger(ButtonEnum button)
        {
            return ReadTrigger(GetButtons(button));
        }
    }
}
