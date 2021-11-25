using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    bool optionsOpen = false;
    public GameObject optionsMenu;
    public Text nbPlayers;

    public Slider nbPlayersSlider;
    // public Slider nbPlayers;
    private void Start() {
        int nb = PlayerPrefs.GetInt("nbPlayers");
        nbPlayersSlider.value = nb;
    }
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

    public void SetPlayers()
    {
        nbPlayers.text =  nbPlayersSlider.value.ToString();
        PlayerPrefs.SetInt("nbPlayers",(int)nbPlayersSlider.value);
    }
}
