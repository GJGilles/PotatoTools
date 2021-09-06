using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace PotatoTools.Character
{
    public static class CharacterService
    {
        public static UnityEvent<CharacterObject, DialogObject> OnDialog = new UnityEvent<CharacterObject, DialogObject>();
        public static UnityEvent<CharacterObject> OnMove = new UnityEvent<CharacterObject>();
        
        private static List<CharacterObject> characters = new List<CharacterObject>();
        private static Dictionary<CharacterObject, Vector2> positions = new Dictionary<CharacterObject, Vector2>();

        static CharacterService()
        {
            characters = AssetLoader.LoadObjects<CharacterObject>();

            // TODO: Save position data
            FileService.Add(new Data().GetService());
        }

        public static Vector2 GetPosition(CharacterObject character)
        {
            return positions[character];
        }

        public static void SetPosition(CharacterObject character, Vector2 pos)
        {
            positions[character] = pos;
            OnMove.Invoke(character);
        }

        public class Data : DataService<Dictionary<int, List<int>>>
        {
            protected override string name => "characters";

            protected override Dictionary<int, List<int>> GetData()
            {
                return characters
                  .ToDictionary(x => x.GetHashCode(), x => x.GetDialogs().Select(y => y.GetHashCode()).ToList());
            }

            protected override void SetData(Dictionary<int, List<int>> data)
            {
                var dialogs = AssetLoader.LoadObjects<DialogObject>().ToDictionary(x => x.GetHashCode());

                for (var i = 0; i < characters.Count; i++)
                {
                    if (data.ContainsKey(characters[i].GetHashCode()))
                    {
                        var c = data[characters[i].GetHashCode()];
                        for (var j = 0; j < c.Count; j--)
                        {
                            characters[i].PushDialog(dialogs[c[j]]);
                        }
                    }
                }
            }
        }
    }
}