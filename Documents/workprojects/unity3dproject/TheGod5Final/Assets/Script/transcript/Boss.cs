using UnityEngine;
using System.Collections;

public class Boss : MonoBehaviour {

	//in the view angle, the boss can attack the player
	public float viewAngle = 50;
	private Transform player;
	public float rotateSpeed = 1;
	public Animator anim;
	public int attackDistance = 3;
	private float distance = 0;
	public float moveSpeed = 2;
	public float timeInterval = 1;
	private float timer = 0;
	private bool isAttacking = false;
	private GameObject attack01GameObject;
	private GameObject attack02GameObject;
	private GameObject attack03GameObject;
	public float[] attackArray = {100,200,300};
	public int hp = 1000;

	// Use this for initialization
	void Start () {
		player = TranscriptManager._instance.player.transform;
		TranscriptManager._instance.enemyList.Add (this.gameObject);
		anim = this.GetComponent<Animator> ();
		attack01GameObject = transform.Find ("attack01").gameObject;
		attack02GameObject = transform.Find ("attack02").gameObject;
		attack03GameObject = transform.Find ("BossBullet").gameObject;
		BossHpProgressBar._instance.Show (hp);
	}
	
	// Update is called once per frame
	void Update () {
		if (hp <= 0) {
			return;
		}
		Distance ();
		Vector3 playerPos = player.position;
		playerPos.y = transform.position.y;	//ensure the angle not influenced by the y axis
		// the angle between the player and the boss
		float angle = Vector3.Angle (player.position - transform.position, transform.forward);
		if (angle < viewAngle / 2) {
			//in the attack angle
			if (distance < attackDistance){
				//attack
//				if(isAttacking == false){
//					anim.SetBool("walk",false);
					timer += Time.deltaTime;
					if(timer > timeInterval){
						Attack();
						timer = 0;
//					}
				}
			} else {
				//move
				anim.SetBool("walk",true);
				rigidbody.MovePosition(transform.position+transform.forward*moveSpeed*Time.deltaTime);
			}
		} else {
			anim.SetBool("walk",true);
//			animation.CrossFade("walk");
			//not in the attack angle, turn to the player direction
			Quaternion targetRotation = Quaternion.LookRotation(player.position - transform.position);
			transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 1*Time.deltaTime);
		}
//		if (distance > attackDistance) {
//			anim.SetBool("walk",true);
//		} else {
//			anim.SetBool("walk",false);
//		}
	}

	void Attack(){
		anim.SetTrigger("attack");
//		isAttacking = true;
	}

	void Distance(){
		distance = Vector3.Distance (player.transform.position, transform.position);
	}

	void PlayAttack01Effect(){
		attack01GameObject.SetActive (true);
		Distance ();
		if (distance < attackDistance) {
			player.SendMessage("TakeDamage",attackArray[0],SendMessageOptions.DontRequireReceiver);
		}
	}

	void PlayAttack02Effect(){
		attack02GameObject.SetActive (true);
		Distance ();
		if (distance < attackDistance) {
			player.SendMessage("TakeDamage",attackArray[1],SendMessageOptions.DontRequireReceiver);
		}
	}

	void PlayAttack03Effect(){
		//attack03GameObject.SetActive (true);
		Distance ();
		if (distance < attackDistance) {
			player.SendMessage("TakeDamage",attackArray[2],SendMessageOptions.DontRequireReceiver);
		}
	}

	//1.how many damage it takes
	//2.in the air and go back distance
	//when get damaged by the boy, invoke this method
	void TakeDamage(string args){
		if (this.hp <= 0) {
			return;		
		}
		Combo._instance.ComboPlus ();
		string[] proArray = args.Split (',');
		//hp minus the damage
		int damage = int.Parse (proArray [0]);
		this.hp -= damage;
		BossHpProgressBar._instance.UpdateShow (hp);
		//TODO update boss health

		//attacked animation
		if(Random.Range(0,100)>95){
			anim.SetTrigger("hit");
		}
		float backDistance = float.Parse(proArray [1]);
		float jumpHeight = float.Parse (proArray [2]);
		if(Random.Range(0,10)>7){
			iTween.MoveBy (this.gameObject, transform.InverseTransformDirection (TranscriptManager._instance.player.transform.forward) * backDistance + Vector3.up * jumpHeight, 0.3f);
		}
		//TODO blood out effect
	
		if (this.hp <= 0) {
			anim.SetBool("die",true);
			Destroy(this.gameObject,2f);
		}
	}

	void Dead(){
//		anim.SetBool("die",true);
//		TranscriptManager._instance.enemyList.Remove (this.gameObject);
//		BossHpProgressBar._instance.Hide ();
//		Destroy (this.gameObject);
//		print("Hello");
	}

//	void Attack(){
//		Transform player = TranscriptManager._instance.player.transform;
//		float distance = Vector3.Distance (transform.position, player.position);
//		if (distance < attackDistance) {
//			player.SendMessage("TakeDamage",damage, SendMessageOptions.DontRequireReceiver);
//		}
//	}
}
