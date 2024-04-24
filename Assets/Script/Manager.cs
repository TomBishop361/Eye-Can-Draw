using System;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;

public class Manager : MonoBehaviour
{
    //Manager Singleton
    public static Manager Instance;

    [Header("Script References")]
    [SerializeField] ClientScript clientScript;
    [SerializeField] NetBehaviour netBehaviour;
    
    

    [Header("UI")]
    public TextMeshProUGUI playercountText;
    [SerializeField] private GameObject[] players;
    [SerializeField] private GameObject[] ingameIcons;       
    [SerializeField] TextMeshProUGUI NextPlayer;
    [SerializeField] TextMeshProUGUI outCome;

    [Header("Unity Events")]
    public UnityEvent Correctevent;
    public UnityEvent Winner;

    //Hidden / Private
    [HideInInspector] public List<GameObject> lines = new List<GameObject>();
    [HideInInspector] public int scoreLimit;
    [HideInInspector] public int playerCount = 2;
    private GameObject Drawer;
    private int drawerIndx;
    private string activePrompt;
    [HideInInspector] public string Guess;

    private void Awake()
    {
        //Singleton
        if (Instance == null)
        {
            Instance = this;
        }
        else { 
            Destroy(Instance); 
        }
    }


    public void morePlayer(){
        //Check if playercount is below maximum players (4)
        if(playerCount < 4){
            //increase player count
            playerCount++;
            playerSelectUIUpdate();
            
        }
    }
   
    public void lessPlayer(){
        //Check playercount is more than minumumplayers required (2)
        if(playerCount > 2){
            //remove a player
            playerCount--;
            playerSelectUIUpdate();
            
        }
    }

    public void undo()
    {
        //Checks if there are lines to undo
        if (lines.Count > 0)
        {
            //gets last line in array and remove it
            GameObject line = lines[lines.Count - 1];
            lines.Remove(line);
            Destroy(line);
        }
    }


    public void answerCheck()
    {
        if (activePrompt != null)
        {
            //compares active prompt to players guess
            if (activePrompt.ToLower() == Guess.ToLower())
            {
                //increase Drawer score
                Drawer.GetComponent<PlayerScoreCounter>().score += 25; // 25 * time multiplier. Faster they guess, more points they get

                //inscrease everyones score and checks if any player has hit point limit
                foreach (GameObject Guesser in ingameIcons)
                {                    
                    PlayerScoreCounter score = Guesser.GetComponent<PlayerScoreCounter>(); //(Drawer is also in Guesser list)
                    score.score += 25;
                    if (score.score >= scoreLimit)
                    {
                        Winner.Invoke();
                        return;
                    }
                }
                Correct(true);
            }
        }
    }

    void playerSelectUIUpdate(){
        //Updates UI to display the right amount of player according to 'playerCount'
        playercountText.text = playerCount.ToString();

        //Disables all player UI icons
        foreach(GameObject Player in players)
        {
            Player.SetActive(false);
        }
        //enables nessisary UI player icons
        for(int i = 0; i < playerCount; i++){
            players[i].SetActive(true);
        }
    }

    public void gerneratePrompt() //function required to call RPC Function
    {
        gerneratePromptRpc();
    }

    [Rpc(SendTo.Server)]
    public void gerneratePromptRpc(){
        // Check if the current instance is the server
        if (NetworkManager.Singleton.IsServer)
        {   
            //Generate prompt
            activePrompt = Enum.GetName(typeof(DrawingPrompt), UnityEngine.Random.Range(0, 87));
            // Notify clients about the prompt change
            netBehaviour.promptChangeClientRpc(activePrompt);
            
        }
    }


    public void Ready(){
        //hide all player UI icons
        foreach (GameObject Player in players){
            Player.SetActive(false);
        }
        //Enable ingame player icons
        for (int i = 0;i < playerCount; i++){
            ingameIcons[i].SetActive(true);
        }
        //Sets player 1 to be drawer
        Drawer = ingameIcons[drawerIndx];
        
    }

    public void Correct(bool correct)
    {        
        Correctevent.Invoke();
        if (correct) outCome.text = ("Correct!");
        else outCome.text = ("Times Up!");
        if (drawerIndx == playerCount)
        {
            drawerIndx = 0;
        }
        else
        {
            drawerIndx++;
        }
        //Drawer = next player in playerlist
        Drawer = ingameIcons[drawerIndx];
        Debug.Log(Drawer.transform.name);
        NextPlayer.text = Drawer.transform.name;
        GameObject[] destoryLines = GameObject.FindGameObjectsWithTag("Line");
        foreach (GameObject destoryLine in destoryLines)
        {
            Destroy(destoryLine);
        }
    }

    

}
