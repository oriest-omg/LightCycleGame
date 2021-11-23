using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    bool isPaused = false;
    GameManager gm;
    public GameObject PauseMenu;
    private void Start() {
        gm =  GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    private void Update() {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(!isPaused)
            {
                isPaused = true;
                gm.gameSpeed =0;
            }else
            {
                isPaused = false;
                gm.gameSpeed =1;
            }
            PauseMenu.SetActive(isPaused);
        }
    }
}
