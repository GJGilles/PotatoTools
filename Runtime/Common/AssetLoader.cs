using System.Collections.Generic;
using UnityEngine;

namespace PotatoTools
{
    public static class AssetLoader
    {
        public static List<T> LoadObjects<T>(string p = "Assets/Objects") where T : UnityEngine.ScriptableObject
        {
#if UNITY_EDITOR
            List<T> result = new List<T>();
            string[] guids = UnityEditor.AssetDatabase.FindAssets("t:" + typeof(T).ToString(), new string[] { p });
            foreach (string g in guids)
            {
                string path = UnityEditor.AssetDatabase.GUIDToAssetPath(g);
                result.Add((T)UnityEditor.AssetDatabase.LoadAssetAtPath(path, typeof(T)));
            }
            return result;
#else
            return new List<T>(Resources.FindObjectsOfTypeAll<T>());
#endif
        }

        public static T LoadAsset<T>(string p) where T : UnityEngine.Object
        {
#if UNITY_EDITOR
            return UnityEditor.AssetDatabase.LoadAssetAtPath<T>("Assets/" + p);
#else
            return Resources.Load<T>(p);
#endif
        }
    }
}