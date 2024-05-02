using System.Collections;
using System.Collections.Generic;
//using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LineScript : MonoBehaviour
{   
    Scene currentScene;
    Color color = Color.black;
    LineRenderer lineRenderer;
    [SerializeField] private EdgeCollider2D _collider2D;
    public Rigidbody2D rb;
    bool _enabled = true;
    public Vector3 newPos;
    float radius;
    private List<Vector2> points = new List<Vector2>();
    // Start is called before the first frame update
    
    public bool physicsScene;
    public const float minDistance = 1.5f;


    void Start()
    {
        //Stores if scene is a physics puzzle
        physicsScene = MyClickBrush.Instance.isPhysicsScene;
        

        lineRenderer = GetComponent<LineRenderer>();
        
        lineRenderer.material.color = color; 
        rb.isKinematic = true;

        
        if (physicsScene)
        {
            //stores radius for circle collider 2D
            radius = lineRenderer.startWidth * 0.5f;

            //sets collider2D false
            _collider2D.enabled = false;
        }
    }

   
    public void colourChange(Color Colour)
    {
        color = Colour;
    }

    //checks if line should be drawn
    private bool canDraw(Vector2 pos)
    {
        //checks line renderer has points
        if (lineRenderer.positionCount == 0) return true;

        //return if distance between current position and last Point is > minDistance (1.5) 
        return Vector2.Distance(lineRenderer.GetPosition(lineRenderer.positionCount - 1),pos) > minDistance;
    }

    //stop drawing and enable physics
    public void lineComplete()
    {
        //sets enabled false (stops linerenderer from adding new points)
        enabled = false;

        
        if (physicsScene)
        {
            //enables physics and 2d Edge collider
            rb.isKinematic = false;
            _collider2D.enabled = true;

            //enables all circle colliders on line
            foreach (CircleCollider2D circle in GetComponents<CircleCollider2D>())
            {
                circle.enabled = true;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {   
        //if drawing is enabled
        if (_enabled)
        {
            //adds offset to newpos
            newPos -= transform.position;

            //if newPos is valid to draw
            if (canDraw(newPos ))
            {
                points.Add(newPos);

                //increase line renderer pos count & adds newPos
                lineRenderer.positionCount++;
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, newPos);

                if (physicsScene)
                {
                    //adds points to collider2d
                    _collider2D.points = points.ToArray();

                    //Add circle collider to allow interation
                    CircleCollider2D circle = this.gameObject.AddComponent<CircleCollider2D>();
                    circle.offset = newPos;
                    circle.radius = radius;
                    circle.enabled = false;
                }
            }
        }
        
    }
}
