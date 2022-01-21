using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenMenu : MonoBehaviour
{

    public GameObject TerminalGUI;


    private void OnTriggerEnter(Collider other)
    {
        TerminalGUI.gameObject.GetComponent<TerminalMenu>().IsInZone = true;
    }


    private void OnTriggerExit(Collider other)
    {
        TerminalGUI.gameObject.GetComponent<TerminalMenu>().IsInZone = false;
    }

}
