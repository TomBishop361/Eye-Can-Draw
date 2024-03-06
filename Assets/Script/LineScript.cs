using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineScript : MonoBehaviour
{
    Color color = Color.black;
    LineRenderer lineRenderer;
    // Start is called before the first frame update
    void Start()
    {
       
       lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.material.color = color;
       //lineRenderer.SetPosition(0, transform.position);
    }

    public void colourChange(Color Colour)
    {
        color = Colour;
    }

    // Update is called once per frame
    void Update()
    {
        //lineRenderer.SetPosition(lineRenderer.positionCount-1, transform.position);
        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount-1, transform.position);
    }
}
