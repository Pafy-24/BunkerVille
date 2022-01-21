using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Gamekit3D
{
    public class GameManager : MonoBehaviour
    {
        public Canvas UI;
        public int TimeInSec;
        public GameObject ChangeScenePressPlate;
        public GameObject Player;

        public GameObject Crate;
        HashSet<KeyValuePair<string, int>> DoorCrate = new HashSet<KeyValuePair<string, int>>();

        private float ElapsedTime = 0;
        static int RemainedSeconds;
        

        public int ReturnRemainedSeconds() { return RemainedSeconds; }

        IEnumerator Timer()
        {
            ElapsedTime = 0;
            while (ElapsedTime <= TimeInSec)
            {
                ElapsedTime += Time.deltaTime;
                RemainedSeconds = TimeInSec - (int)ElapsedTime;

                yield return null;
            }
            EndMiniGame();
        }

        IEnumerator CountDown()
        {
            GameObject[] Destroyer;
            ElapsedTime = 0;
            while (ElapsedTime <= 3)
            {
                Destroyer = GameObject.FindGameObjectsWithTag("Counter");
                for (int j = 0; j < Destroyer.Length; j++)
                {
                    Destroy(Destroyer[j].gameObject);
                }

                ElapsedTime += Time.deltaTime;
                //RemainedSeconds = TimeInSec - (int)ElapsedTime;

                GameObject Counter = new GameObject("Start Counter", typeof(RectTransform));
                Counter.tag = "Counter";
                Counter.transform.SetParent(UI.transform);
                Text myText = Counter.AddComponent<Text>();
                Counter.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                Counter.GetComponent<RectTransform>().sizeDelta = new Vector2(500f, 500f);
                Counter.GetComponent<RectTransform>().anchorMin += new Vector2(0f , 0f);
                Counter.GetComponent<RectTransform>().anchorMax += new Vector2(0f, 0f);

                int value = (int)3 - (int)ElapsedTime;
                myText.text = value.ToString();
                myText.fontSize = 750;
                myText.color = Color.red;
                myText.font = Font.CreateDynamicFontFromOSFont("Arial", 750);
                myText.alignment = TextAnchor.MiddleCenter;

                yield return null;
            }

            Destroyer = GameObject.FindGameObjectsWithTag("Counter");
            for (int j = 0; j < Destroyer.Length; j++)
            {
                Destroy(Destroyer[j].gameObject);
            }

            Text text = UI.transform.Find("Timer").GetComponent<Text>();
            text.enabled = true;
            Player.GetComponent<PlayerInput>().enabled = true;
            StartCoroutine(Timer());
        }

        void Start()
        {
            Player.GetComponent<PlayerInput>().enabled = false;
            DoorCrate = Crate.gameObject.GetComponent<DoorInventory>().ReturnInventory();
            StartCoroutine(CountDown());

        }

        // Update is called once per frame
        void Update()
        {
            Text text = UI.transform.Find("Timer").GetComponent<Text>();
           // Debug.Log(HasTime);
            text.text = RemainedSeconds.ToString() + " Seconds";
        }
        
        public void EndMiniGame()
        {
            UI.transform.Find("Timer").GetComponent<Text>().enabled = false;
            ChangeScenePressPlate.SetActive(true);
            StartCoroutine(BreakTime());
        }
        IEnumerator BreakTime()
        {
            yield return new WaitForSeconds(2);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadScene(3);

        }

    }
}