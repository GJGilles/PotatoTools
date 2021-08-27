using PotatoTools.Inventory;
using System;

namespace PotatoTools.Character
{
    public static class PlayerService
    {
        private static int locks = 0;

        private static int money = 0;
        private static ItemInventory inventory = new ItemInventory(8);

        static PlayerService()
        {
            FileService.Add(new Data().GetService());
        }

        public static bool IsLocked() => locks > 0;
        public static void Lock() => locks++;
        public static void Unlock() => locks--;

        public static int GetMoney()
        {
            return money;
        }

        public static ItemInventory GetInventory()
        {
            return inventory;
        }

        public static void AddMoney(int amount)
        {
            money += amount;
        }


        [Serializable]
        public class PlayerData
        {
            public int money;
            public InventoryData inventory;
        }

        public class Data : DataService<PlayerData>
        {
            protected override string name => "player";

            protected override PlayerData GetData()
            {
                return new PlayerData()
                {
                    money = money,
                    inventory = inventory.Save()
                };
            }

            protected override void SetData(PlayerData data)
            {
                money = data.money;
                inventory.Load(data.inventory);
            }
        }
    }
}