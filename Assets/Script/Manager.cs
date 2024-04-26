using System;
using System.Collections.Generic;
using System.Linq;
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
    [SerializeField] TimerScript timerScript;
    
    

    [Header("UI")]
    public TextMeshProUGUI playercountText;
    [SerializeField] private GameObject PlayerSelect;
    [SerializeField] private TMP_Text promptText;
    [SerializeField] private GameObject ReadyButton;
    [SerializeField] private GameObject[] players;
    [SerializeField] private GameObject[] ingameIcons;
    [SerializeField] private GameObject[] winnerIcons;
    [SerializeField] TextMeshProUGUI NextPlayer;
    [SerializeField] TextMeshProUGUI outCome;
    public int gameTimer = 80;

    [Header("Unity Events")]
    public UnityEvent Correctevent;
    public UnityEvent Winner;

    //Hidden / Private
    [HideInInspector] public List<GameObject> lines = new List<GameObject>();
    [HideInInspector]public int scoreLimit = 150;
    [HideInInspector] public int playerCount = 2;
    private GameObject Drawer;
    private int drawerIndx;
    private string activePrompt;
    [HideInInspector] public string Guess;
    [HideInInspector]public int bonus = 20;
    [HideInInspector] bool localPlay = false;
    List<PlayerScoreCounter> winners = new List<PlayerScoreCounter>();

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

    
    //Clears all lines
    public void clear()
    {
        Debug.Log(lines.Count);
        if (lines.Count > 0)
        {
            for(int i = 0; i < lines.Count; i++) 
            {
                Destroy(lines[i].gameObject);
                
            }
            lines.Clear();
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
            if (activePrompt.ToLower() == Guess.ToLower().Replace(" ", ""))
            {
                PlayerScoreCounter drawerScore = Drawer.GetComponent<PlayerScoreCounter>();
                drawerScore.score += 30;
                //inscrease everyones score and checks if any player has hit point limit
                //foreach (GameObject Guesser in ingameIcons)
                Debug.Log(ingameIcons.Count());
                for (int i = 0; i < ingameIcons.Count(); i++)
                {                    
                    PlayerScoreCounter score = ingameIcons[i].GetComponent<PlayerScoreCounter>(); //(Drawer is also in Guesser list)
                    if(score != drawerScore && score.playing) score.score += 25 + bonus;
                    if (score.score >= scoreLimit) {
                        winners.Add(score);
                    }
                }
                if(winners.Count > 0)
                {
                    GameWin();
                }
                else
                {
                    Correct(true);
                }                
                
            }
        }
    }


    private void GameWin()
    {
        Winner.Invoke();
        for (int i = 0;i < winners.Count;i++)
        {
            Debug.Log(winners.Count);
            winnerIcons[winners[i].player].SetActive(true);
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
        //Generate prompt
        activePrompt = Enum.GetName(typeof(DrawingPrompt), UnityEngine.Random.Range(0, 87));
        if (!localPlay)
        {
            promptText.text = "Check Your Prompt!";
            gerneratePromptRpc();
        }
        else
        {
            promptText.text = activePrompt;
        }
    }

    [Rpc(SendTo.Server)]
    public void gerneratePromptRpc(){
        // Check if the current instance is the server
        if (NetworkManager.Singleton.IsServer)
        {              
           netBehaviour.promptChangeClientRpc(activePrompt);
                        
            
        }
    }

    //sets game to be playable without a connected devices
    public void playLocal()
    {
        localPlay = true;
        PlayerSelect.SetActive(true);
        ReadyButton.SetActive(true);
    }

    //Called when a devices is connected
    public void DeviceConnected()
    {
        ReadyButton.SetActive(true);
    }

    public void Ready(){
        //hide all player UI icons
        foreach (GameObject Player in players){
            Player.SetActive(false);
        }
        //Enable ingame player icons
        for (int i = 0;i < playerCount; i++){
            ingameIcons[i].SetActive(true);
            ingameIcons[i].GetComponent<PlayerScoreCounter>().playing = true;
        }
        //Sets player 1 to be drawer
        Drawer = ingameIcons[drawerIndx];
        
    }
    
    //Starts the in game timer
    public void startTimer()
    {
        timerScript.startTimer(gameTimer);
    }

    //Called if answer is correct
    public void Correct(bool correct)
    {        
        Correctevent.Invoke();
        if (correct) outCome.text = ("Correct!");
        else outCome.text = ("Times Up!");
        if (drawerIndx == playerCount-1)
        {
            drawerIndx = 0;
        }
        else
        {
            drawerIndx++;
        }
        //Drawer = next player in playerlist
        Drawer = ingameIcons[drawerIndx];
        NextPlayer.text = Drawer.transform.name;
        GameObject[] destoryLines = GameObject.FindGameObjectsWithTag("Line");
        foreach (GameObject destoryLine in destoryLines)
        {
            Destroy(destoryLine);
        }
    }

    

}
