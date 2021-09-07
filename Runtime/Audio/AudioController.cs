using System.Collections;
using UnityEngine;

namespace PotatoTools.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioController : MonoBehaviour
    {
        public AudioTypeEnum type = AudioTypeEnum.SFX;
        [Range(0f, 1f)] public float volume = 0.5f;

        private void Start()
        {
            AudioService.OnVolumeChange.AddListener(ChangeVolume);
            ChangeVolume(type);
        }

        private void OnDestroy()
        {
            AudioService.OnVolumeChange.RemoveListener(ChangeVolume);
        }

        private void ChangeVolume(AudioTypeEnum t)
        {
            if (t == type)
                GetComponent<AudioSource>().volume = AudioService.GetVolume(type, volume);
        }
    }
}