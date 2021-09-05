using System;
using UnityEngine;

namespace PotatoTools.Dialog
{
    [Serializable]
    public class DialogBlock
    {
        [TextArea(5, 6)]
        public string text;
        public int speaker;
    }
}