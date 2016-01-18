using UnityEngine;
using System.Collections;

public class PlayerAutoMove : MonoBehaviour {

	private NavMeshAgent agent;
	public float minDistance = 3;

	public Transform target;

	// Use this for initialization
	void Start () {
		agent = this.GetComponent<NavMeshAgent> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (agent.enabled) {
			if(agent.remainingDistance<minDistance){
				agent.Stop();
				agent.enabled = false;
			}
		}

		if (Input.GetKeyDown (KeyCode.Q)) {
			SetDestination(target.position);		
		}

		float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis("Vertical");
		if (Mathf.Abs (h) > 0.05f || Mathf.Abs (v) > 0.05f) {
			StopAuto();	//in the auto run, if any button pressed, stop the auto run
		}
	}

	public void SetDestination(Vector3 targetPos){
		agent.enabled = true;
		agent.SetDestination (targetPos);

	}

	public void StopAuto(){
		if (agent.enabled) {
			agent.Stop();
			agent.enabled = false;
		}
	}
}
