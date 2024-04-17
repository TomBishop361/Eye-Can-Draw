using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineScript : MonoBehaviour
{
    Color color = Color.black;
    LineRenderer lineRenderer;
    [SerializeField] private EdgeCollider2D _collider2D;

    private List<Vector2> points = new List<Vector2>();
    // Start is called before the first frame update

    public const float RESOLUTION = 1.5f;
    void Start()
    {
       
       lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.material.color = color;
        
    }

    public void colourChange(Color Colour)
    {
        color = Colour;
    }

    private bool canDraw(Vector2 pos)
    {
        if (lineRenderer.positionCount == 0) return true;
        return Vector2.Distance(lineRenderer.GetPosition(lineRenderer.positionCount - 1),pos) > RESOLUTION;
    }


    // Update is called once per frame
    void Update()
    {

        if (canDraw(transform.position))
        {
            points.Add(transform.position);

            lineRenderer.positionCount++;
            lineRenderer.SetPosition(lineRenderer.positionCount - 1, transform.position);

            _collider2D.points = points.ToArray();
            _collider2D.transform.localPosition = new Vector2(0 - transform.position.x, 0 - transform.position.y);
        }
    }
}
