﻿using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace PotatoTools.Inventory
{
    public enum StackMoveEnum
    {
        One,
        Half,
        All
    }

    public class ItemInventory
    {
        private int capacity = 6;
        private bool infinite = false;

        private List<ItemStack> items = new List<ItemStack>();
        private int selected = 0;

        public UnityEvent<int> OnChange = new UnityEvent<int>();

        public ItemInventory(int size, bool inf = false)
        {
            capacity = size;
            items = new List<ItemStack>(new ItemStack[capacity]);
            infinite = inf;
        }

        public int GetSize() { return capacity; }

        public int GetLocation() { return selected; }

        public int Find(ItemObject item)
        {
            return items.FindIndex(i => i != null && i.item == item);
        }

        public ItemStack Peek(int? idx = null)
        {
            if (idx == null)
            {
                idx = selected;
            }

            if (items[(int)idx] == null)
            {
                return null;
            }
            else
            {
                return items[(int)idx];
            }
        }

        public ItemStack Remove(StackMoveEnum amount, int? idx = null)
        {
            int i = idx == null ? selected : (int)idx;

            if (items[i] == null)
            {
                return null;
            }
            else
            {
                int count;
                switch (amount)
                {
                    default:
                    case StackMoveEnum.All:
                        count = items[i].number;
                        break;

                    case StackMoveEnum.Half:
                        count = items[i].number / 2;
                        break;

                    case StackMoveEnum.One:
                        count = 1;
                        break;
                }
                count = Mathf.Min(count, items[i].number);

                ItemStack output = count > 0 ? new ItemStack(items[i].item, count) : null;

                items[i].number -= count;
                if (items[i].number <= 0 && !items[i].permanent)
                {
                    items[i] = null;
                }

                OnChange.Invoke(i);
                return output;
            }
        }

        public ItemStack Add(StackMoveEnum amount, ItemStack stack, int? idx = null)
        {
            int i = idx == null ? selected : (int)idx;

            if (items[i] == null)
            {
                items[i] = new ItemStack(stack.item, 0);
            }
            
            if (items[i].item != stack.item)
            {
                if (amount == StackMoveEnum.All && !items[i].permanent)
                {
                    var ret = items[i];
                    items[i] = stack;
                    OnChange.Invoke(i);
                    return ret;
                }
                else
                    return stack;
            }
            else
            {
                int diff;
                switch (amount)
                {
                    default:
                    case StackMoveEnum.All:
                        diff = stack.number;
                        break;
                    case StackMoveEnum.Half:
                        diff = stack.number / 2;
                        break;
                    case StackMoveEnum.One:
                        diff = 1;
                        break;
                }

                int next = infinite ? items[i].number + diff : Mathf.Min(items[i].number + diff, stack.item.stack);
                stack.number -= next - items[i].number;
                items[i].number = next;
                OnChange.Invoke(i);

                if (stack.number <= 0)
                {
                    return null;
                }
                else
                {
                    return stack;
                }
            }
        }

        public bool CanPush(ItemStack stack)
        {
            int num = stack.number; 
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i] != null && items[i].item != stack.item)
                {
                    continue;
                }
                else
                {
                    int space = items[i] == null ? 10 : items[i].item.stack - items[i].number;
                    if (space >= num)
                    {
                        return true;
                    }
                    else
                    {
                        num -= space;
                    }
                }
            }

            return false;
        }

        public ItemStack TryPush(ItemStack stack)
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i] != null && items[i].item != stack.item)
                {
                    continue;
                }
                else
                {
                    stack = Add(StackMoveEnum.All, stack, i);
                    if (stack == null)
                    {
                        break;
                    }
                }
            }
            return stack;
        }

        public bool CanPull(ItemStack stack)
        {
            int num = stack.number; 
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i] == null || items[i].item != stack.item)
                {
                    continue;
                }
                else
                {
                    int count = items[i].number;
                    if (count >= num)
                    {
                        return true;
                    }
                    else
                    {
                        num -= count;
                    }
                }
            }

            return false;
        }

        public ItemStack TryPull(ItemStack stack)
        {
            int num = stack.number;
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i] == null || items[i].item != stack.item)
                {
                    continue;
                }
                else
                {
                    int count = items[i].number;
                    if (count >= num)
                    {
                        for (int j = 0; j < num; j++) Remove(StackMoveEnum.One, i);
                        return stack;
                    }
                    else
                    {
                        Remove(StackMoveEnum.All, i);
                        num -= count;
                    }
                }
            }

            if (num == 0) return null;

            stack.number -= num;
            return stack;
        }

        public ItemStack TryPull()
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i] == null)
                {
                    continue;
                }
                else
                {
                    return Remove(StackMoveEnum.All, i);
                }
            }
            return null;
        }

        public void SetPermanent(int idx, ItemObject item)
        {
            infinite = true;
            items[idx] = new ItemStack(item, 0) { permanent = true };
        }

        public void SetSelect(int idx)
        {
            selected = idx;
            while (selected >= items.Count) selected -= items.Count;
            while (selected < 0) selected += items.Count;
        }

        public InventoryData Save()
        {
            return new InventoryData()
            {
                capacity = capacity,
                infinite = infinite,
                items = items.Select(x => new StackData() { item = x.item.GetHashCode(), number = x.number }).ToList()
            };
        }

        public void Load(InventoryData data)
        {
            capacity = data.capacity;
            infinite = data.infinite;

            items = data.items.Select(x => x == null ? null : new ItemStack(ItemService.Get(x.item), x.number)).ToList();
        }
    }

    [Serializable]
    public class StackData
    {
        public int item;
        public int number;
    }

    [Serializable]
    public class InventoryData
    {
        public int capacity;
        public bool infinite;

        public List<StackData> items;
    }
}