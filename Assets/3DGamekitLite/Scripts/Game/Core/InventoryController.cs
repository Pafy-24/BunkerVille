using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace Gamekit3D
{
    public class InventoryController : MonoBehaviour
    {

        public int MaxInventoryStorage = 5;
        public GameObject InGameUI;

        [System.Serializable]
        public class InventoryEvent
        {
            public string key;
            public UnityEvent OnAdd, OnRemove;
        }

        [System.Serializable]
        public class InventoryChecker
        {

            public string[] inventoryItems;
            public UnityEvent OnHasItem, OnDoesNotHaveItem;

            public bool CheckInventory(InventoryController inventory)
            {

                if (inventory != null)
                {
                    for (var i = 0; i < inventoryItems.Length; i++)
                    {
                        if (!inventory.HasItem(inventoryItems[i]))
                        {
                            OnDoesNotHaveItem.Invoke();
                            return false;
                        }
                    }
                    OnHasItem.Invoke();
                    return true;
                }
                return false;
            }
        }


        public InventoryEvent[] inventoryEvents;

        HashSet<KeyValuePair<string, int>> inventoryItems = new HashSet<KeyValuePair<string, int>>();

        public int StorageOccupied()
        {
            RefreshInventory();
            int storage = 0;
            foreach (var iv in inventoryItems)
            {
                storage += iv.Value;
            }

            return storage;
        }

        public void AddItem(string key, int space)
        {
            //Debug.Log("Start Adding");
            var ev = GetInventoryEvent(key);
            if (ev != null) 
                ev.OnAdd.Invoke();
            
            //Debug.Log("Executed OnAdd");
            bool keyIsContained = false;

            foreach (var iv in inventoryItems)
            {
                if (iv.Key == key)
                {
                    int value = (int)iv.Value + space;
                    inventoryItems.Remove(iv);
                    inventoryItems.Add(new KeyValuePair<string, int>(key, value));
                    keyIsContained = true;
                    break;
                }

            }
            if(keyIsContained == false)
                inventoryItems.Add(new KeyValuePair<string, int>(key, space));

            RefreshInventory();
            //  ConsoleShowInventory();

        }

        public string[] ReturnTypeofItems()
        {
            string[] Items = { "AidKit", "Food", "Water", "Gun", "GasMask"};
            return Items;
        }

        public void RemoveItem(string key)
        {
            if (inventoryItems.Contains(new KeyValuePair<string, int>(key, int.MinValue)))
            {
                var ev = GetInventoryEvent(key);
                if (ev != null) ev.OnRemove.Invoke();
                inventoryItems.Remove(new KeyValuePair<string, int>(key, int.MinValue));
            }
            RefreshInventory();
        }

        public void RefreshInventory()
        {
            InGameUI.GetComponent<HealthUI>().UpdateInventory();
        }

        public HashSet<KeyValuePair<string, int>> ReturnInventory()
        {
            return inventoryItems;
        }

        public bool HasItem(string key)
        {
            InGameUI.GetComponent<HealthUI>().UpdateInventory();
            return inventoryItems.Contains(new KeyValuePair<string, int>(key, int.MinValue));
        }

        public void Clear()
        {
            inventoryItems.Clear();
            RefreshInventory();
        }

        InventoryEvent GetInventoryEvent(string key)
        {
            foreach (var iv in inventoryEvents)
            {
                if (iv.key == key) return iv;
            }
            return null;
        }

    }

}