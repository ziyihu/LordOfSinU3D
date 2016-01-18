using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BossBullet : MonoBehaviour {

	public float moveSpeed = 3;
	public float Damage = 300;
	private List<GameObject> playerList = new List<GameObject>();
	private float repeatRate = 0.5f;
	private float distance;
	// Use this for initialization
	void Start () {
		InvokeRepeating("Attack",0,repeatRate);
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += transform.forward * moveSpeed * Time.deltaTime;
	}

	void Distance(){
		distance = Vector3.Distance (transform.position, TranscriptManager._instance.player.transform.position);
	}

	void Attack(){
		Distance ();
		if(distance<2.5){
			GameObject player = TranscriptManager._instance.player;
			player.SendMessage("TakeDamage",Damage*repeatRate,SendMessageOptions.DontRequireReceiver);
		}
	}
}
