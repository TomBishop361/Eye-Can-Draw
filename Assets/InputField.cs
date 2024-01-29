using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InputFeild : MonoBehaviour
{
    public TMP_InputField feild;
    public Manager manager;
    
    // Start is called before the first frame update
    void OnEnable(){
        feild.ActivateInputField();
    }

    public void Enter()
    {
        manager.Guess = feild.text;
        manager.answerCheck();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
