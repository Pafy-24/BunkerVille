using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace Gamekit3D
{
    [RequireComponent(typeof(Collider))]
    public class GoToTheNextLevel : MonoBehaviour
    {
        public static HashSet<KeyValuePair<string, int>> Inventory;
        public GameObject Door;
        void OnTriggerEnter(Collider other)
        {
            Inventory = Door.GetComponent<DoorInventory>().ReturnInventory();
            SceneManager.LoadScene(2);
        }

        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}