using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	// blood effect
	public GameObject damageEffectPrefab;
	public int hp = 500;
	private int hpTotal = 0;
	private Transform bloodPoint;
	public float speed = 1;
	//attack fluence, how many seconds per attack
	public int attackRate = 2;
	private float attackTimer = 0;	//count timer,next attack time
	public float attackDistance = 1.8f; 
	//if the enemy die, move it underground. the speed
	private float downSpeed = 1f;
	private float downDistance = 0;
	private float distance = 0;
	private Transform hpBarPoint;
	//control the enemy move
	private CharacterController cc;
	//hp slider
	private GameObject hpBarGameObject;
	private UISlider hpBarSlider;
	//hp damage number show
	private GameObject hudTextGameObject;
	private HUDText hudText;
	public int damage = 20;

	void Start(){
		hpTotal = hp;
		bloodPoint = transform.Find("BloodPoint");
		cc = this.GetComponent<CharacterController> ();
		InvokeRepeating ("CalcDistance",0,0.1f);
		hpBarPoint = transform.Find("HpBarPoint");
		hpBarGameObject = HpBarManager._instance.GetHpBar (hpBarPoint.gameObject);
		hpBarSlider = hpBarGameObject.transform.Find ("Bg").GetComponent<UISlider>();

		hudTextGameObject = HpBarManager._instance.GetHudText (hpBarPoint.gameObject);
		hudText = hudTextGameObject.GetComponent<HUDText> ();
	}

	void Update(){
		if (hp <= 0) {
			//move the enemy underground
			downDistance +=  downSpeed * Time.deltaTime;
			transform.Translate(-transform.up * downSpeed * Time.deltaTime);
			if(downDistance > 4){
				Destroy(this.gameObject);
			}
			return;
		}
		if (hp <= 0) {
			return;
		}
		if (distance < attackDistance) {
			attackTimer += Time.deltaTime;
			if(attackTimer > attackRate){
				//attack
				Transform player = TranscriptManager._instance.player.transform;
				//get the player position
				Vector3 targetPos = player.position;
				targetPos.y = transform.position.y;
				//face toward the player
				transform.LookAt (targetPos);
				animation.Play("attack01");
				attackTimer = 0;
			}
			if(!animation.IsPlaying("attack01")){
				animation.CrossFade("idle");
			}
		} else {
			animation.Play("walk");
			Move ();
		}
	}

	//Enemy move, enemy move toward the player
	void Move(){
		//get the player
		Transform player = TranscriptManager._instance.player.transform;
		//get the player position
		Vector3 targetPos = player.position;
		targetPos.y = transform.position.y;
		//face toward the player
		transform.LookAt (targetPos);
		cc.SimpleMove (transform.forward * speed);
	}

	void CalcDistance(){
		Transform player = TranscriptManager._instance.player.transform;
		distance = Vector3.Distance (player.position, transform.position);
	}

	//1.how many damage it takes
	//2.in the air and go back distance
	//when get damaged by the boy, invoke this method
	void TakeDamage(string args){
		if (hp <= 0) {
			return;		
		}
		Combo._instance.ComboPlus ();
		string[] proArray = args.Split (',');
		//hp minus the damage
		int damage = int.Parse (proArray [0]);
		hp -= damage;
		//slider value
		hpBarSlider.value = (float)hp / hpTotal;
		//hp damage number show
		hudText.Add ("-"+damage,Color.red,0.3f);
		//attacked animation
		animation.Play("takedamage");
		float backDistance = float.Parse(proArray [1]);
		float jumpHeight = float.Parse (proArray [2]);
		iTween.MoveBy (this.gameObject, transform.InverseTransformDirection (TranscriptManager._instance.player.transform.forward) * backDistance + Vector3.up * jumpHeight, 0.3f);
		//blood out effect
		GameObject.Instantiate (damageEffectPrefab, bloodPoint.transform.position, Quaternion.identity);
		if (hp <= 0) {
			Dead();
		}
	}

	void Attack(){
		Transform player = TranscriptManager._instance.player.transform;
		float distance = Vector3.Distance (transform.position, player.position);
		if (distance < attackDistance) {
			player.SendMessage("TakeDamage",damage, SendMessageOptions.DontRequireReceiver);
		}
	}

	//when the enemy dead
	void Dead(){
		TranscriptManager._instance.enemyList.Remove (this.gameObject);
		this.GetComponent<CharacterController> ().enabled = false;
		Destroy (hpBarGameObject);
		Destroy (hudTextGameObject);
		//die method 1, play the die animation
		//die method 2, the body in pieces
		int random = Random.Range (0, 10);
		if (random <= 7) {
			animation.Play("die");
		}
		else {
			this.GetComponentInChildren<MeshExploder> ().Explode ();
			this.GetComponentInChildren<SkinnedMeshRenderer> ().enabled = false;
			Destroy (this.gameObject);
		}
	}

}
