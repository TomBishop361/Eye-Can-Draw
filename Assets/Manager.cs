using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class Manager : MonoBehaviour
{
    
    public TextMeshProUGUI playercountText;
    public int playerCount = 2;
    public string Guess;
    private string activePrompt;
    [SerializeField] private GameObject[] players;
    [SerializeField] private GameObject[] ingameIcons;
    private GameObject Drawer;
    private int drawerIndx;
    public UnityEvent Correctevent;
    [SerializeField] TextMeshProUGUI PromptText;
    [SerializeField] TextMeshProUGUI NextPlayer;
    

    private void Start()
    {
        
    }
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
        if(activePrompt.ToLower() == Guess.ToLower()){
            foreach ( GameObject Guesser in ingameIcons){
                Guesser.GetComponent<PlayerScoreCounter>().score += 25;
            }
            Drawer.GetComponent<PlayerScoreCounter>().score += 25; // 25 * time multiplier. Faster they guess, more points they get
            Correct();
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


    public void gerneratePrompt(){
        activePrompt = Enum.GetName(typeof(DrawingPrompt), UnityEngine.Random.Range(0, 87));
        PromptText.text = activePrompt;
    }


    public void Ready(){
        foreach (GameObject Player in players){
            Player.SetActive(false);
        }
        for (int i = 0;i < playerCount; i++){
            ingameIcons[i].SetActive(true);
        }
        Drawer = ingameIcons[drawerIndx];
    }

    public void Correct()
    {
        Debug.Log("Correct =)");
        Correctevent.Invoke();
        if (drawerIndx == playerCount -1)
        {
            drawerIndx = 0;
        }
        else
        {
            drawerIndx++;
        }
        Drawer = ingameIcons[drawerIndx];
        NextPlayer.text = Drawer.transform.name;
        GameObject[] destoryLines = GameObject.FindGameObjectsWithTag("Line");
        foreach (GameObject destoryLine in destoryLines)
        {
            Destroy(destoryLine);
        }
    }

    public void TimeOut()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
