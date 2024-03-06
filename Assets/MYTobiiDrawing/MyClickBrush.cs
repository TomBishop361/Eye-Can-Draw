using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.Gaming;

public class MyClickBrush : MonoBehaviour {

	public GameObject GazePoint;
	public GameObject brushPrefab;
	public GameObject FollowObj;
	GameObject thisBrush;
	Vector2 startPos;
	Plane objPlane;
	public Color color;

	//Vector3 prevPos;

	public float multiplier = 0.3f;
	private Vector2 _historicPoint;
	private bool _hasHistoricPoint;

    // Called at the start of the scene
	void Start(){
		objPlane = new Plane(Camera.main.transform.forward*-1, this.transform.position);	
    }

	
    public static float lerp(float startValue, float endValue, float t)
    {
        return (startValue + (endValue - startValue) * t);
    }

    // Update is called once per frame
    void Update () {

		GazePoint gazePoint = TobiiAPI.GetGazePoint();      
		
        if (Input.GetKeyDown (KeyCode.Mouse1)) {

			//Instanciates the Trail object to the FollowObj So that the trail starts at the eye position each time
            thisBrush = (GameObject)Instantiate(brushPrefab, FollowObj.transform.position, Quaternion.identity);
			thisBrush.GetComponent<LineScript>().colourChange(color);
            Ray mRay = Camera.main.ScreenPointToRay (gazePoint.Screen);

			float rayDistance;
            _hasHistoricPoint = false;
			if (objPlane.Raycast (mRay, out rayDistance)) {
				startPos = mRay.GetPoint (rayDistance);
				
			}
		} else if (Input.GetKey (KeyCode.Mouse1) && gazePoint.IsRecent ()) {
			Ray mRay = Camera.main.ScreenPointToRay (gazePoint.Screen);

			float rayDistance;
			if (objPlane.Raycast (mRay, out rayDistance)) {
				//Follow Obj and Brush follows the Eye Position 
				FollowObj.transform.position = smoothFilter(new Vector2(mRay.GetPoint(rayDistance).x,mRay.GetPoint(rayDistance).y));
                thisBrush.transform.position = smoothFilter(new Vector2(mRay.GetPoint(rayDistance).x, mRay.GetPoint(rayDistance).y));

            }

			//prevPos = mRay.GetPoint (rayDistance);
		} else if (Input.GetKey (KeyCode.Mouse1)) {
			if (Vector2.Distance (thisBrush.transform.position, startPos) < 0.1)
            {
                Destroy (thisBrush);
                _hasHistoricPoint = false;
            }
		}
		//my bit
		else {
			//While pen is up (Space is not pressed)
            Ray mRay = Camera.main.ScreenPointToRay(gazePoint.Screen);
            float rayDist;
			//Follow Obj will follow Eye Position at all times
            if (objPlane.Raycast(mRay, out rayDist))
            {
				FollowObj.transform.position = smoothFilter(new Vector2(mRay.GetPoint(rayDist).x, mRay.GetPoint(rayDist).y));    
				
            }
        }
		
	}

	private Vector2 smoothFilter(Vector2 point){
		if (!_hasHistoricPoint) {
			_historicPoint = point;
			_hasHistoricPoint = true;
		}

		var smoothedPoint = new Vector2 (point.x * multiplier + _historicPoint.x * (1.0f - multiplier), 
			                    point.y * multiplier + _historicPoint.y * (1.0f - multiplier));

		_historicPoint = smoothedPoint;

		return smoothedPoint;
	}

	public void colourRed()
	{
		color = Color.red;
	}
	public void colourGreen()
	{
		color = Color.green;
	}
	public void colourBlue()
	{
		color = Color.blue;
	}

	public void colourYellow() {
		color = Color.yellow;
	}



}
