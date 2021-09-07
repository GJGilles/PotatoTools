using System.Collections;
using UnityEngine;

namespace PotatoTools.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioOnceController : AudioController
    {
        private void Update()
        {
            if (!GetComponent<AudioSource>().isPlaying)
            {
                Destroy(gameObject);
            }
        }
    }
}