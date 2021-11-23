using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    bool optionsOpen = false;
    public GameObject optionsMenu;
    public void play()
    {
        SceneManager.LoadScene("Game");
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void Options()
    {
        if(!optionsOpen)
        {
            optionsOpen = true;
        }else
        {
            optionsOpen = false;
        }
        optionsMenu.SetActive(optionsOpen);
    }
}
