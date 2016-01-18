using UnityEngine;
using System.Collections;

public class PlayerVillageMove : MonoBehaviour {

	//moving speed
	public float velocity = 5;
	private NavMeshAgent agent;
	public Transform target;
	

	// Update is called once per frame
	void Update () {
		//control the character move
		float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis("Vertical");
		//move speed
		Vector3 vel = rigidbody.velocity;
		rigidbody.velocity = new Vector3 (-h*velocity, vel.y, -v*velocity);

		//move direction
		if (Mathf.Abs (h) > 0.05f || Mathf.Abs (v) > 0.05f) {
			//change the head direction
			//direction:   new Vector3(-h,0,-v);
			transform.rotation = Quaternion.LookRotation (new Vector3 (-h, 0, -v));
		} 
		//not holding the button, stop running immediately
		else {
			if(agent.enabled==false){
				rigidbody.velocity = Vector3.zero;
			}
		}
		if (agent.enabled) {
			transform.rotation = Quaternion.LookRotation (agent.velocity);
		}


	}
}
