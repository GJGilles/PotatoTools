using System.Collections;
using UnityEngine;

namespace PotatoTools
{
    public static class CommonAnimation
    {
        public static bool DampedMove(Vector2 a, Vector2 b, out Vector2 c, float rate = 0.03f, float threshold = 0.1f) 
        {
            rate *= Time.deltaTime * 100;
            c = Vector2.Lerp(a, b, rate);
            return Mathf.Abs((a - b).magnitude) < threshold;
        }

        public static bool LinearMove(Vector2 a, Vector2 b, out Vector2 c, float speed)
        {
            if (Mathf.Abs((b - a).magnitude) < speed)
            {
                c = b;
                return true;
            }
            else
            {
                c = a + (b - a).normalized * speed;
                return false;
            }
        }
    }
}