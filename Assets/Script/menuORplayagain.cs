using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuORplayagain : MonoBehaviour
{
    
    public void playAgain()
    {
        string scene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(scene);
    }

    public void backToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
