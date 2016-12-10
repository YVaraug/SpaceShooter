using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	public float maxSpeed = 5f;
	public float rotSpeed = 180f;

	float shipBoundaryRadius = 0.5f;

	void Start () {


	}
	
	void Update () 
	{

		//Face the mouse
		FacetToMouse (rotSpeed);
		// Move in forward relative to player
		Vector3 velocity = new Vector3(0, Input.GetAxis("Vertical") * maxSpeed * Time.deltaTime, 0);
		transform.position += transform.rotation * velocity;
		// Avoid hitting the camara Ortographic Boundaries
		RestrictToBoundaries (shipBoundaryRadius);


	}




	void FacetToMouse(float rotSpeed)
	{
		
		// Gets the mouse positions and transforms it into world units from pixels
		Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		// Calculates the vector between the ship and the mouse position
		Vector3 dir = mousePos - transform.position;
		dir.Normalize(); 
		// Calculates angle of the vector and creates quaternion with the right angle
		float zAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
		Quaternion desiredRot = Quaternion.Euler( 0, 0,zAngle );
		// Rotates towards desired rotation with rotation speed
		transform.rotation = Quaternion.RotateTowards( transform.rotation, desiredRot, rotSpeed * Time.deltaTime);


	}

	void WASDMovement(float maxSpeed, float rotSpeed)

	{
		// ROTATE the ship.

		// Grab our rotation quaternion
		Quaternion rot = transform.rotation;
		// Grab the Z euler angle
		float z = rot.eulerAngles.z;
		// Change the Z angle based on input
		z -= Input.GetAxis("Horizontal") * rotSpeed * Time.deltaTime;
		// Recreate the quaternion
		rot = Quaternion.Euler( 0, 0, z );
		// Feed the quaternion into our rotation
		transform.rotation = rot;
		Vector3 pos = transform.position;
		Vector3 velocity = new Vector3(0, Input.GetAxis("Vertical") * maxSpeed * Time.deltaTime, 0);
		pos += transform.rotation * velocity;

	}


	void RestrictToBoundaries(float objectRadius)

	{

		// RESTRICT the player to the camera's boundaries!
		Vector3 pos = transform.position;
		// First to vertical, because it's simpler
		if(pos.y+objectRadius > Camera.main.orthographicSize) {
			pos.y = Camera.main.orthographicSize - objectRadius;
		}
		if(pos.y-objectRadius < -Camera.main.orthographicSize) {
			pos.y = -Camera.main.orthographicSize + objectRadius;
		}

		// Now calculate the orthographic width based on the screen ratio
		float screenRatio = (float)Screen.width / (float)Screen.height;
		float widthOrtho = Camera.main.orthographicSize * screenRatio;

		// Now do horizontal bounds
		if(pos.x+objectRadius > widthOrtho) {
			pos.x = widthOrtho - objectRadius;
		}
		if(pos.x-objectRadius < -widthOrtho) {
			pos.x = -widthOrtho + objectRadius;
		}

		// Finally, update our position!!
		transform.position = pos;

	}

	void ZoomInOut(int maxOrthoSize = 20, int minOrthoSize = 5)
	{
		// Gets the input of the Scroll Wheel
		float scrollButton = Input.mouseScrollDelta.y;
		// Gets the actual Ortho Size
		float orthoSize = Camera.main.orthographicSize;
		// Zooms in or out till a Max or Min
		if (scrollButton > 0 && orthoSize < maxOrthoSize) {
			Camera.main.orthographicSize++;
		} 
		else if (scrollButton < 0 && orthoSize > minOrthoSize) {
			Camera.main.orthographicSize--;
		}
	}

}
