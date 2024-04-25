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
    
    bool physicsScene;
    public const float RESOLUTION = 1.5f;
    void Start()
    {
        currentScene = SceneManager.GetActiveScene();
        physicsScene = (currentScene == SceneManager.GetSceneByName("2DPhysics"));
        Debug.Log("Starting");
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.material.color = color;
        rb.isKinematic = true;
        if (physicsScene)
        {
            radius = lineRenderer.startWidth * 0.5f;
            _collider2D.enabled = false;
        }
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

    //stop drawing and enable physics
    public void lineComplete()
    {
        
        enabled = false;
        if (physicsScene)
        {
            rb.isKinematic = false;
            _collider2D.isTrigger |= false;
            foreach (CircleCollider2D circle in GetComponents<CircleCollider2D>())
            {
                circle.enabled = true;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_enabled)
        {
            newPos -= transform.position;
            if (canDraw(newPos ))
            {
                points.Add(newPos);

                lineRenderer.positionCount++;
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, newPos);

                if (physicsScene)
                {
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
