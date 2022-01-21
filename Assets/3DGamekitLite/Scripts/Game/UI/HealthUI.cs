using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace Gamekit3D
{
    public class HealthUI : MonoBehaviour
    {

        public bool SeeHearts;
        public Damageable representedDamageable;
        public GameObject healthIconPrefab;
        public GameObject WaterIconPrefab;
        public GameObject FoodIconPrefab;
        public GameObject AidKitIconPrefab;
        public GameObject Player;

        protected Animator[] m_HealthIconAnimators;

        protected readonly int m_HashActivePara = Animator.StringToHash("Active");
        protected readonly int m_HashInactiveState = Animator.StringToHash("Inactive");
        protected const float k_HeartIconAnchorWidth = 0.041f;
        protected const float k_IconAnchorHeight = 0.15f;

        IEnumerator Start()
        {
            if (SeeHearts == true)
            {
                if (representedDamageable == null)
                    yield break;

                yield return null;

                m_HealthIconAnimators = new Animator[representedDamageable.maxHitPoints];
                int i;
                for (i = 0; i < representedDamageable.maxHitPoints; i++)
                {
                    GameObject healthIcon = Instantiate(healthIconPrefab);
                    healthIcon.transform.SetParent(transform);
                    RectTransform healthIconRect = healthIcon.transform as RectTransform;
                    healthIconRect.anchoredPosition = Vector2.zero;
                    healthIconRect.sizeDelta = Vector2.zero;
                    healthIconRect.anchorMin += new Vector2(k_HeartIconAnchorWidth, 0f) * i;
                    healthIconRect.anchorMax += new Vector2(k_HeartIconAnchorWidth, 0f) * i;
                    m_HealthIconAnimators[i] = healthIcon.GetComponent<Animator>();

                    if (representedDamageable.currentHitPoints < i + 1)
                    {
                        m_HealthIconAnimators[i].Play(m_HashInactiveState);
                        m_HealthIconAnimators[i].SetBool(m_HashActivePara, false);
                    }
                }

            }
        }

        private void Update()
        {
        }

        public void UpdateInventory()
        {
            HashSet<KeyValuePair<string, int>> inventory = Player.GetComponent<InventoryController>().ReturnInventory();

            int i = 0;
            string[] TypeofItem = Player.GetComponent<InventoryController>().ReturnTypeofItems();
            foreach (var ToI in TypeofItem)
            {
                GameObject[] Destroyer;
                Destroyer = GameObject.FindGameObjectsWithTag(ToI);
                for (int j = 0; j < Destroyer.Length; j++)
                {
                    Destroy(Destroyer[j].gameObject);
                }
            }

            foreach (var item in inventory)
            {   int value = item.Value;
                if(item.Key == "Water")
                {
                    i = i+1;
                    GameObject WaterIcon = Instantiate(WaterIconPrefab);
                    WaterIcon.tag = item.Key;
                    WaterIcon.transform.SetParent(transform);
                    RectTransform IconRect = WaterIcon.transform as RectTransform;
                    IconRect.anchoredPosition = Vector2.zero;
                    IconRect.sizeDelta = Vector2.zero;
                    IconRect.anchorMin += new Vector2(0, -k_IconAnchorHeight * i + k_HeartIconAnchorWidth);
                    IconRect.anchorMax += new Vector2(0, -k_IconAnchorHeight * i + k_HeartIconAnchorWidth);

                    GameObject Value = new GameObject("WaterValue", typeof(RectTransform));
                    Value.tag = item.Key;
                    Value.transform.SetParent(transform);
                    Text myText = Value.AddComponent<Text>();
                    Value.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                    Value.GetComponent<RectTransform>().sizeDelta = new Vector2(500f, 500f);
                    Value.GetComponent<RectTransform>().anchorMin += new Vector2(k_IconAnchorHeight-0.5f - k_HeartIconAnchorWidth, -k_IconAnchorHeight * i + 0.5f);
                    Value.GetComponent<RectTransform>().anchorMax += new Vector2(k_IconAnchorHeight-0.5f - k_HeartIconAnchorWidth, -k_IconAnchorHeight * i + 0.5f);

                    myText.text = "X"+item.Value.ToString();
                    myText.fontSize = 32;
                    myText.color = Color.red;
                    myText.font = Font.CreateDynamicFontFromOSFont("Arial", 32);
                    myText.alignment = TextAnchor.MiddleCenter;

                }

                if(item.Key == "Food")
                {
                    i =i+1;
                    GameObject FoodIcon = Instantiate(FoodIconPrefab);
                    FoodIcon.tag = item.Key;
                    FoodIcon.transform.SetParent(transform);
                    RectTransform IconRect = FoodIcon.transform as RectTransform;
                    IconRect.anchoredPosition = Vector2.zero;
                    IconRect.sizeDelta = Vector2.zero;
                    IconRect.anchorMin += new Vector2(0 , -k_IconAnchorHeight * i + k_HeartIconAnchorWidth);
                    IconRect.anchorMax += new Vector2(0 , -k_IconAnchorHeight * i + k_HeartIconAnchorWidth);

                    GameObject Value = new GameObject("FoodValue", typeof(RectTransform));
                    Value.tag = item.Key;
                    Value.transform.SetParent(transform);
                    Text myText = Value.AddComponent<Text>();
                    Value.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                    Value.GetComponent<RectTransform>().sizeDelta = new Vector2(500f, 500f);
                    Value.GetComponent<RectTransform>().anchorMin += new Vector2(k_IconAnchorHeight - 0.5f - k_HeartIconAnchorWidth, -k_IconAnchorHeight * i + 0.5f);
                    Value.GetComponent<RectTransform>().anchorMax += new Vector2(k_IconAnchorHeight - 0.5f - k_HeartIconAnchorWidth, -k_IconAnchorHeight * i + 0.5f);

                    myText.text = "X" + item.Value.ToString();
                    myText.fontSize = 32;
                    myText.color = Color.red;
                    myText.font = Font.CreateDynamicFontFromOSFont("Arial", 32);
                    myText.alignment = TextAnchor.MiddleCenter;

                }
                
                if (item.Key == "AidKit")
                {
                    int StorageUnit = 1;

                    GameObject[] PickableItems;
                    PickableItems = GameObject.FindGameObjectsWithTag("Pickable");
                    foreach(var itemValues in PickableItems)
                    {
                        if (itemValues.name == item.Key)
                        {
                            StorageUnit = itemValues.GetComponent<InventoryItem>().Storge;
                            break;
                        }
                    }

                    i = i + 1;
                    GameObject AidKitIcon = Instantiate(AidKitIconPrefab);
                    AidKitIcon.tag = item.Key;
                    AidKitIcon.transform.SetParent(transform);
                    RectTransform IconRect = AidKitIcon.transform as RectTransform;
                    IconRect.anchoredPosition = Vector2.zero;
                    IconRect.sizeDelta = Vector2.zero;
                    IconRect.anchorMin += new Vector2(0, -k_IconAnchorHeight * i + k_HeartIconAnchorWidth);
                    IconRect.anchorMax += new Vector2(0, -k_IconAnchorHeight * i + k_HeartIconAnchorWidth);

                    GameObject Value = new GameObject("AidKitValue", typeof(RectTransform));
                    Value.tag = item.Key;
                    Value.transform.SetParent(transform);
                    Text myText = Value.AddComponent<Text>();
                    Value.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                    Value.GetComponent<RectTransform>().sizeDelta = new Vector2(500f, 500f);
                    Value.GetComponent<RectTransform>().anchorMin += new Vector2(k_IconAnchorHeight - 0.5f - k_HeartIconAnchorWidth, -k_IconAnchorHeight * i + 0.5f);
                    Value.GetComponent<RectTransform>().anchorMax += new Vector2(k_IconAnchorHeight - 0.5f - k_HeartIconAnchorWidth, -k_IconAnchorHeight * i + 0.5f);

                    myText.text = "X" + (int)(item.Value / 2);
                    myText.fontSize = 32;
                    myText.color = Color.red;
                    myText.font = Font.CreateDynamicFontFromOSFont("Arial", 32);
                    myText.alignment = TextAnchor.MiddleCenter;

                }
                // Debug.Log(i);
            }
        }

        public void ChangeHitPointUI(Damageable damageable)
        {
            if (m_HealthIconAnimators == null)
                return;

            for (int i = 0; i < m_HealthIconAnimators.Length; i++)
            {
                m_HealthIconAnimators[i].SetBool(m_HashActivePara, damageable.currentHitPoints >= i + 1);
            }
        }
    } 
}
