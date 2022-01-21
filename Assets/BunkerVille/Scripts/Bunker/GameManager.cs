using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
namespace SpaceVille
{
    public class GameManager : MonoBehaviour
    {
        public static int DaysSpent;
        public Canvas UI;
        public GameObject Player;
        void Start()
        {
            DaysSpent = 0;
        }

        public void Sleep()
        {
            StartCoroutine(NextDay());
        }

        public IEnumerator NextDay() { 
            DaysSpent++;



            GameObject BG = new GameObject("CutScene", typeof(RectTransform));
            BG.tag = "Counter";
            BG.transform.SetParent(UI.transform);
            Image BGimage = BG.AddComponent<Image>();
            BG.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            BG.GetComponent<RectTransform>().sizeDelta = new Vector2(5000f, 5000f);
            BG.GetComponent<RectTransform>().anchorMin += new Vector2(0f, 0f);
            BG.GetComponent<RectTransform>().anchorMax += new Vector2(0f, 0f);

            BGimage.color = Color.black;




            GameObject Message = new GameObject("CutSceneText", typeof(RectTransform));
            Message.tag = "Counter";
            Message.transform.SetParent(UI.transform);
            Text myText = Message.AddComponent<Text>();
            Message.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            Message.GetComponent<RectTransform>().sizeDelta = new Vector2(500f, 500f);
            Message.GetComponent<RectTransform>().anchorMin += new Vector2(0f, 0f);
            Message.GetComponent<RectTransform>().anchorMax += new Vector2(0f, 0f);

            myText.text = "Sleep...";
            myText.fontSize = 50;
            myText.color = Color.red;
            myText.font = Font.CreateDynamicFontFromOSFont("Arial", 50);
            myText.alignment = TextAnchor.MiddleCenter;

            yield return new WaitForSeconds(3);
            myText.text = "Day " + (int)(DaysSpent+1);


            yield return new WaitForSeconds(2);
            GameObject[] Destroyer = GameObject.FindGameObjectsWithTag("Counter");
            for (int j = 0; j < Destroyer.Length; j++)
            {
                Destroy(Destroyer[j].gameObject);
            }


        }

        IEnumerator BreakTimeCutScenes()
        {
            yield return new WaitForSeconds(2);
            
        }

        void Update()
        {

        }
    }
}
