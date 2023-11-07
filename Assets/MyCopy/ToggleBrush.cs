using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.Gaming;

public class ToggleBrush : MonoBehaviour {

	public GameObject GazePoint;
	public GameObject brushPrefab;
	GameObject thisBrush;
	Vector2 startPos;
	Plane objPlane;

	//Vector3 prevPos;

	public float multiplier = 0.3f;
	private Vector2 _historicPoint;
	private bool _hasHistoricPoint;

    // Called at the start of the scene
	void Start(){
		objPlane = new Plane(Camera.main.transform.forward*-1, this.transform.position);
		//moved to create bruch at start instead of in update
		thisBrush = (GameObject)Instantiate(brushPrefab, this.transform.position, Quaternion.identity);
	}


	//lerp function to move the brush to eye Position
	public static float lerp(float startValue, float endValue, float t)
	{
		return (startValue + (endValue - startValue) * t);
	}

	// Update is called once per frame
	void Update () {

		GazePoint gazePoint = TobiiAPI.GetGazePoint();

		//Moves the instance of bruch when pressing space instead of creating bruch every time
		lerp(thisBrush.transform.position.x, this.transform.position.x, 0);
		lerp(thisBrush.transform.position.y, this.transform.position.y, 0);


		if (Input.GetKeyDown (KeyCode.Space)) {			

			Ray mRay = Camera.main.ScreenPointToRay (gazePoint.Screen);

			float rayDistance;
            _hasHistoricPoint = false;
			if (objPlane.Raycast (mRay, out rayDistance)) {
				startPos = mRay.GetPoint (rayDistance);
				//prevPos = mRay.GetPoint (rayDistance);
			}
		} else if (Input.GetKey (KeyCode.Space) && gazePoint.IsRecent ()) {
			Ray mRay = Camera.main.ScreenPointToRay (gazePoint.Screen);

			float rayDistance;
			if (objPlane.Raycast (mRay, out rayDistance)) {
				thisBrush.transform.position = smoothFilter(new Vector2(mRay.GetPoint(rayDistance).x,mRay.GetPoint(rayDistance).y));
			}

			//prevPos = mRay.GetPoint (rayDistance);
		} else if (Input.GetKey (KeyCode.Space)) {
			if (Vector2.Distance (thisBrush.transform.position, startPos) < 0.1)
            {
                Destroy (thisBrush);
                _hasHistoricPoint = false;
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
}
