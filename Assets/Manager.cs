using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public TextMeshProUGUI playercountText;
    public int playerCount = 2;
    public string Guess;
    private string activePrompt;
    [SerializeField] private GameObject[] players;
    [SerializeField] private GameObject[] ingameIcons;
    private GameObject[] Guessers;
    private GameObject Drawer;
  

    public void morePlayer(){
        if(playerCount < 4){
            playerCount++;
            playerSelectUIUpdate();
            
        }
    }

    public void lessPlayer(){
        if(playerCount > 2){
            playerCount--;
            playerSelectUIUpdate();
            
        }
    }

    public void answerCheck(){
        if(activePrompt == Guess.ToLower()){
            foreach ( GameObject Guesser in Guessers){
                Guesser.GetComponent<PlayerData>().score += 25;
            }
            Drawer.GetComponent<PlayerData>().score += 25; // 25 * time multiplier. Faster they guess, more points they get
        }
    }

    void playerSelectUIUpdate(){
        playercountText.text = playerCount.ToString();
        foreach(GameObject Player in players)
        {
            Player.SetActive(false);
        }
        for(int i = 0; i < playerCount; i++){
            players[i].SetActive(true);
        }
    }

    public void Ready(){
        foreach (GameObject Player in players){
            Player.SetActive(false);
        }
        for (int i = 0;i < playerCount; i++){
            ingameIcons[i].SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
