using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace PotatoTools.Audio
{
    public enum AudioTypeEnum
    {
        BGM,
        SFX
    }

    public static class AudioService
    {
        private static int master = 50;
        private static Dictionary<AudioTypeEnum, int> volumes = new Dictionary<AudioTypeEnum, int>();

        public static UnityEvent<AudioTypeEnum> OnVolumeChange = new UnityEvent<AudioTypeEnum>();

        static AudioService()
        {
            volumes = new List<AudioTypeEnum>((AudioTypeEnum[])Enum.GetValues(typeof(AudioTypeEnum))).ToDictionary(x => x, x => 50);
        }

        public static float GetVolume(AudioTypeEnum type, float local)
        {
            return (local * master * volumes[type]) / (100 * 100);
        }

        public static void SetMaster(int vol)
        {
            master = vol;
            foreach (var t in Enum.GetValues(typeof(AudioTypeEnum)))
            {
                OnVolumeChange.Invoke((AudioTypeEnum)t);
            }
        } 

        public static void SetVolume(AudioTypeEnum type, int vol)
        {
            volumes[type] = vol;
            OnVolumeChange.Invoke(type);
        }

        public static void Play(AudioClip clip, AudioTypeEnum type)
        {
            var source = new GameObject().AddComponent<AudioSource>();
            source.transform.SetParent(Camera.main.transform);
            source.clip = clip;
            source.Play();

            source.gameObject.AddComponent<AudioOnceController>().type = type;
        }
    }
}