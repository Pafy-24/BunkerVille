using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Collider))]
public class DoorInventory : MonoBehaviour
{

    public Gamekit3D.InventoryController.InventoryEvent[] inventoryEvents;
    public HashSet<KeyValuePair<string, int>> Collected = new HashSet<KeyValuePair<string, int>>();

    [HideInInspector]
    new public Collider collider;



    void OnTriggerEnter(Collider other)
    {

        HashSet<KeyValuePair<string, int>> Aux = new HashSet<KeyValuePair<string, int>>();

        var ic = other.GetComponent<Gamekit3D.InventoryController>();
        Aux = ic.ReturnInventory();

        foreach (var item in Aux)
        {
            gameObject.transform.Find("Open").gameObject.SetActive(true);
            AddItem(item.Key, item.Value);
        }
        ic.Clear();
    }

    public HashSet<KeyValuePair<string, int>> ReturnInventory()
    {
        return Collected;
    }
    public void AddItem(string key, int storage)
    {
        var ev = GetInventoryEvent(key);
        if (ev != null)
            ev.OnAdd.Invoke();

        bool keyIsContained = false;
        foreach (var iv in Collected)
        {
            if (iv.Key == key)
            {
                int value = iv.Value + storage;
                Collected.Remove(iv);
                Collected.Add(new KeyValuePair<string, int>(key, value));
                keyIsContained = true;
                break;
            }

        }
        if (keyIsContained == false)
            Collected.Add(new KeyValuePair<string, int>(key, storage));

    }

    public void DebugInventory()
    {
        Debug.Log("Collected:");
        foreach (var iv in Collected)
        {
            Debug.Log(iv.Key + " " + iv.Value);
        }
        Debug.Log("-----------------------");

    }
    Gamekit3D.InventoryController.InventoryEvent GetInventoryEvent(string key)
    {
        foreach (var iv in inventoryEvents)
        {
            if (iv.key == key) return iv;
        }
        return null;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
