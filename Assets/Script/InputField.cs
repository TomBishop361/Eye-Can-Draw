using TMPro;
using UnityEngine;


public class InputField : MonoBehaviour
{
    public TMP_InputField feild;
    
    
    // Start is called before the first frame update
    void OnEnable(){
        feild.ActivateInputField();
        
    }

  
    //calls answer check
    public void Enter()
    {
        Manager.Instance.Guess = feild.text;
        Manager.Instance.answerCheck();

    }

}
