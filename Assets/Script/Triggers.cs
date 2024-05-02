using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Triggers : MonoBehaviour
{
    public UnityEvent triggerEvent;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("ball")){
            triggerEvent.Invoke();
        }        
    }
}
