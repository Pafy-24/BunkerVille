using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Gamekit3D
{
    [RequireComponent(typeof(Collider))]
    public class InventoryItem : MonoBehaviour, IDataPersister
    {
        public string inventoryKey;
        public int Storge;
        public Canvas canvas;
        

        public LayerMask layers;
        public bool disableOnEnter = false;


        [HideInInspector]
        new public Collider collider;

        public AudioClip clip;
        public DataSettings dataSettings;

        void OnEnable()
        {
            collider = GetComponent<Collider>();
            PersistentDataManager.RegisterPersister(this);
        }

        void OnDisable()
        {
            PersistentDataManager.UnregisterPersister(this);
        }

        void Reset()
        {
            layers = LayerMask.NameToLayer("Everything");
            collider = GetComponent<Collider>();
            collider.isTrigger = true;
            dataSettings = new DataSettings();
        }
        IEnumerator HideText()
        {
            yield return new WaitForSeconds(3f);
            canvas.transform.Find("FullInventoryText").GetComponent<Text>().enabled = false;
        }

        void OnTriggerEnter(Collider other)
        {
           // Debug.Log("Triggered");
            if (layers.Contains(other.gameObject))
            {
                var ic = other.GetComponent<InventoryController>();

                if (ic.StorageOccupied() >= ic.MaxInventoryStorage)
                {
                    this.disableOnEnter = false;
                    canvas.transform.Find("FullInventoryText").GetComponent<Text>().text = " Your Inventory Is Full! (" + ic.MaxInventoryStorage + "/" + ic.MaxInventoryStorage + ")";
                    canvas.transform.Find("FullInventoryText").GetComponent<Text>().enabled = true;
                    StartCoroutine(HideText());
                }
                else { 
                    ic.AddItem(inventoryKey, Storge);
                    disableOnEnter = true;
                }
               // Debug.Log("Element Added");
                if (disableOnEnter)
                {
                    gameObject.SetActive(false);
                    Save();
                }

                if (clip) AudioSource.PlayClipAtPoint(clip, transform.position);

                ic.RefreshInventory();

            }
        }

        public void Save()
        {
            PersistentDataManager.SetDirty(this);
        }

        void OnDrawGizmos()
        {
            Gizmos.DrawIcon(transform.position, "InventoryItem", false);
        }

        public DataSettings GetDataSettings()
        {
            return dataSettings;
        }

        public void SetDataSettings(string dataTag, DataSettings.PersistenceType persistenceType)
        {
            dataSettings.dataTag = dataTag;
            dataSettings.persistenceType = persistenceType;
        }

        public Data SaveData()
        {
            return new Data<bool>(gameObject.activeSelf);
        }

        public void LoadData(Data data)
        {
            Data<bool> inventoryItemData = (Data<bool>)data;
            gameObject.SetActive(inventoryItemData.value);
        }
    }
}
