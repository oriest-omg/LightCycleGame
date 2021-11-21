using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    int nbAlivePlayers;
    [HideInInspector]
    public float gameSpeed = 1;
    public int[] score;
    Player[] players;
    public GameObject goTxt;
    // Start is called before the first frame update
    void Start()
    {
        players = FindObjectsOfType<Player>();
        nbAlivePlayers = players.Length;
    }
    public void killPlayer()
    {
        nbAlivePlayers--;
        if(nbAlivePlayers <= 1 ){
            gameSpeed = 0;
            GetWinner();
        }
    }
    void GetWinner()
    {
        foreach(Player p in players)
        {
            if(p.isAlive)
            {
                switch(p.playerName)
                {
                    case "P1":
                        score[0]++;
                        GameObject.Find("P1score").GetComponent<Text>().text = "Score "+ score[0];
                        break;
                    case "P2":
                        score[1]++;
                        GameObject.Find("P2score").GetComponent<Text>().text = "Score "+ score[1];
                        break;
                    case "P3":
                        score[2]++;
                        break;
                    case "P4":
                        score[3]++;
                        break;
                }
            }
        }
        //reset game
        StartCoroutine("ResetGame");
    }

    IEnumerator ResetGame()
    {
        yield return new WaitForSeconds(2);
        goTxt.SetActive(true);
        nbAlivePlayers = players.Length;
        gameSpeed = 1;
        GameObject [] walls = GameObject.FindGameObjectsWithTag("wall");
        foreach(GameObject go in walls)
        {
            Destroy(go);
        }
        foreach(Player p in players)
        {
            p.gameObject.SetActive(true);
            p.ResetPlayer();
        }
        Invoke("hideGoTxt",1);
    }
    void hideGoTxt()
    {
        goTxt.SetActive(false);
    }
}
