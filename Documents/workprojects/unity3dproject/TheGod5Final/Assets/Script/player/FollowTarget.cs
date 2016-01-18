using UnityEngine;
using System.Collections;

public class FollowTarget : MonoBehaviour {

	public Vector3 offset;
	private Transform playerBip;
	public float smooting = 4;

	// Use this for initialization
	void Start () {
		playerBip = GameObject.FindGameObjectWithTag ("Player").transform.Find("Bip01");
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Vector3 targetPos = playerBip.position + offset;
		transform.position = Vector3.Lerp(transform.position,targetPos,smooting*Time.deltaTime);
	}
}
