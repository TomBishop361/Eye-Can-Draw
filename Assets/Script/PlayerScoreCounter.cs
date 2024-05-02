using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerScoreCounter : MonoBehaviour
{
    public bool playing;
    public int player;
    public int score;
    [SerializeField]TextMeshProUGUI scoreText;
    

    //set score to 0
    void Start()
    {
        score = 0;
    }

    //Update Score Ui
    public void OnGUI()
    {
        scoreText.text = score.ToString();
    }

}
