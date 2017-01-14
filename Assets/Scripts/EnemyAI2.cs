using UnityEngine;
using System.Collections;

public class EnemyAI2 : MonoBehaviour {

	public Transform target;

	public float maxSpeed = 5f;
	public float rotSpeed = 90f;
	public Vector3 bulletOffset = new Vector3(0, 0.5f, 0);
	public GameObject bulletPrefab;
	int bulletLayer;
	public float fireDelay = 0.50f;
	float cooldownTimer = 0;

	Vector2[] path;
	int targetIndex;

	void Start() {
		StartCoroutine (RefreshPath ());
	}

	IEnumerator RefreshPath() {
		Vector2 targetPositionOld = (Vector2)target.position + Vector2.up; // ensure != to target.position initially

		while (true) {
			if (targetPositionOld != (Vector2)target.position) {
				targetPositionOld = (Vector2)target.position;

				path = Pathfinding.RequestPath (transform.position, target.position);
				StopCoroutine ("FollowPath");
				StartCoroutine ("FollowPath");
			}

			yield return new WaitForSeconds (.25f);
		}
	}

	IEnumerator FollowPath() {
		if (path.Length > 0) {
			targetIndex = 0;
			Vector2 currentWaypoint = path [0];

			while (true) {
				if (Vector2.Distance((Vector2)transform.position, currentWaypoint)<0.2f) {
					targetIndex++;
					if (targetIndex >= path.Length) {
						yield break;
					}
					currentWaypoint = path [targetIndex];
				}
				MoveToTarget (new Vector3(currentWaypoint.x,currentWaypoint.y,0));
				cooldownTimer -= Time.deltaTime;
				if (cooldownTimer <= 0) {
					Shooting (bulletPrefab, bulletOffset);
					cooldownTimer = fireDelay;

				}

				yield return null;

			}
		}
	}

	void MoveToTarget (Vector3 target){
		FaceGOWithTag (target,rotSpeed);
		MoveForward (maxSpeed);
	}

	void MoveForward(float speed)
	{
		//Gets the position of the GameObject, creates 
		//a velocity vector and then rotates it to 
		//finally apply it to the the GO transform

		Vector3 pos = transform.position;
		Vector3 velocity = new Vector3(0, speed * Time.deltaTime, 0);
		pos += transform.rotation * velocity;
		transform.position = pos;	

	}

	void FaceGOWithTag(Vector3 target,float rotSpeed)
	{

		Vector3 dir = target - transform.position;
		dir.Normalize();

		float zAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;

		Quaternion desiredRot = Quaternion.Euler( 0, 0,zAngle );

		transform.rotation = Quaternion.RotateTowards( transform.rotation, desiredRot, rotSpeed * Time.deltaTime);
	}

	void Shooting(GameObject bullet,Vector3 offset, string tag = "Player", float distance = 4.0f)

	{
		//Find the player and return if not found
		GameObject go = GameObject.FindWithTag (tag);
		if(go == null)
			return;	// Try again next frame!

		//If at shooting distance from target rotate offset 
		//and shoot bullet in self.layer
		if( Vector3.Distance(transform.position, go.transform.position) < distance) {

			Vector3 turnedOffset = transform.rotation * offset;
			GameObject bulletGO = (GameObject)Instantiate(bullet, transform.position + turnedOffset, transform.rotation);
			bulletGO.layer = gameObject.layer;
		}

	}

	public void OnDrawGizmos() {
		if (path != null) {
			for (int i = targetIndex; i < path.Length; i ++) {
				Gizmos.color = Color.black;
				//Gizmos.DrawCube((Vector3)path[i], Vector3.one *.5f);

				if (i == targetIndex) {
					Gizmos.DrawLine(transform.position, path[i]);
				}
				else {
					Gizmos.DrawLine(path[i-1],path[i]);
				}
			}
		}
	}
}