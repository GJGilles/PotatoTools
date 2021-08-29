using System;
using UnityEngine;

namespace PotatoTools.Dialog
{
    [Serializable]
    public class DialogBlock
    {
        [TextArea]
        public string text;
        public int speaker;
    }
}