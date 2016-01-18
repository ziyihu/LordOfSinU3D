using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour {

	public float velocity = 5;
	private Animator anim;
	private PlayerAttack playerAttack;

	// Use this for initialization
	void Start () {
		anim = this.GetComponent<Animator> ();
		playerAttack = this.GetComponent<PlayerAttack> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (playerAttack.hp <= 0) {
			return;
		}
		float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis("Vertical");
		Vector3 nowVel = rigidbody.velocity;
		if (Mathf.Abs (h) > 0.05f || Mathf.Abs (v) > 0.05f) {
			anim.SetBool("Move",true);
			if(anim.GetCurrentAnimatorStateInfo(1).IsName("Empty State")){
				rigidbody.velocity = new Vector3(velocity*h,nowVel.y,velocity*v);
				transform.LookAt(new Vector3(h,0,v)+transform.position);
			} else {
				rigidbody.velocity = new Vector3(0,nowVel.y,0);
			}
		} else {
			anim.SetBool("Move",false);
			rigidbody.velocity = new Vector3(0,nowVel.y,0);
		}
	}
}
