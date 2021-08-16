using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace PotatoTools
{

    public static class FileService
    {
        private static int slot = 0;
        private static List<DataService> services = new List<DataService>();

        private static string GetPath(string slot)
        {
            return Application.persistentDataPath + "/" + slot + "/";
        }

        public static void Add(DataService data)
        {
            services.Add(data);
        }

        public static bool Save()
        {
            Directory.CreateDirectory(GetPath(slot.ToString()));

            foreach (var s in services)
            {
                s.Save(GetPath(slot.ToString()) + s.name);
            }

            return true;
        }

        public static bool Load()
        {
            if (!Directory.Exists(GetPath(slot.ToString())))
            {
                return false;
            }

            foreach (var s in services)
            {
                s.Load(GetPath(slot.ToString()) + s.name);
            }

            return true;
        }
    }
}