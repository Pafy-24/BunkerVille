using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject OptionsMenu;
    public void Exit()
    {
        Application.Quit();
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
    }
    public void MainMenu_Options()
    {
        OptionsMenu.SetActive(true);
        this.gameObject.SetActive(false);
    }
    public void Options_MainMenu()
    {
        OptionsMenu.SetActive(false);
        this.gameObject.SetActive(true);
    }


}
