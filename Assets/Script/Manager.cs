using System;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;

public class Manager : MonoBehaviour
{
    [SerializeField] ClientScript clientScript;
    [SerializeField] NetBehaviour netBehaviour;
    public TextMeshProUGUI playercountText;
    public int playerCount = 2;
    public string Guess;
    private string activePrompt;
    [SerializeField] private GameObject[] players;
    [SerializeField] private GameObject[] ingameIcons;
    private GameObject Drawer;
    private int drawerIndx;
    public List<GameObject> lines = new List<GameObject>();
    public UnityEvent Correctevent;
    public UnityEvent Winner;
    [SerializeField] TextMeshProUGUI PromptText;
    [SerializeField] TextMeshProUGUI NextPlayer;
    [SerializeField] TextMeshProUGUI outCome;


    public void morePlayer(){
        if(playerCount < 4){
            playerCount++;
            playerSelectUIUpdate();
            
        }
    }

    public void undo()
    {
        if (lines.Count > 0)
        {
            GameObject line = lines[lines.Count - 1];
            lines.Remove(line);
            Destroy(line);
        }
    }
    public void lessPlayer(){
        if(playerCount > 2){
            playerCount--;
            playerSelectUIUpdate();
            
        }
    }

    public void answerCheck()
    {
        if (activePrompt != null)
        {
            if (activePrompt.ToLower() == Guess.ToLower())
            {

                Drawer.GetComponent<PlayerScoreCounter>().score += 25; // 25 * time multiplier. Faster they guess, more points they get
                foreach (GameObject Guesser in ingameIcons)
                {
                    PlayerScoreCounter score = Guesser.GetComponent<PlayerScoreCounter>();
                    score.score += 25;
                    if (score.score >= 100)
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
        playercountText.text = playerCount.ToString();
        foreach(GameObject Player in players)
        {
            Player.SetActive(false);
        }
        for(int i = 0; i < playerCount; i++){
            players[i].SetActive(true);
        }
    }

    public void gerneratePrompt()
    {
        gerneratePromptRpc();
    }

    [Rpc(SendTo.Server)]
    public void gerneratePromptRpc(){
        if (NetworkManager.Singleton.IsServer)
        {   
            activePrompt = Enum.GetName(typeof(DrawingPrompt), UnityEngine.Random.Range(0, 87));
            netBehaviour.promptChangeClientRpc(activePrompt);
            //testClientRpc();
        }
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
