using PotatoTools.Inventory;

namespace PotatoTools.Character
{
    public static class PlayerService
    {
        private static int locks = 0;

        private static int money = 0;
        private static ItemInventory inventory = new ItemInventory(8);

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
    }
}