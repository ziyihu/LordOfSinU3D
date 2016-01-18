using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerAttack : MonoBehaviour {

	private Dictionary<string, PlayerEffect> effectDict = new Dictionary<string, PlayerEffect>();
	public PlayerEffect[] effectArray;
//	public List<GameObject> enemyList = new List<GameObject>();
	public float distanceAttackForward = 1.5f;
	public float distanceAttackAround = 4f;
	// damage: normal, skill1, skill2, skill3
	public int[] damageArray = new int[] {20,30,30,30};
	public enum AttackRange
		{
			Forward,
			Around
		}
	public int hp = 1000;

	private Animator anim;
	
	private GameObject hudTextGameObject;
	private HUDText hudText;
	private Transform damageShowPoint;

	
	//0 normal skill1 skill2 skill3
	//1 effect name 
	//2 sound name
	//3 move forward
	//4 jump height
	void Attack(string args) {
		string[] proArray = args.Split('.');
		//1 show effect
		string effectName = proArray[1];
		ShowPlayerEffect(effectName);
		//2 play sound
		string soundName = proArray [2];
		SoundManager._instance.Play (soundName);
		//3 move forward 
		float moveForward = float.Parse(proArray [3]);
		if (moveForward > 0.1f) {
			iTween.MoveBy(this.gameObject,Vector3.forward*moveForward,0.3f);
		}
		string posType = proArray [0];
		if(posType == "normal"){
			ArrayList array = GetEnemyInAttackRange(AttackRange.Forward);
			foreach(GameObject go in array){
				go.SendMessage("TakeDamage",damageArray[0]+","+proArray[3]+","+proArray[4]);
			}
		} else if (posType == "skill1"){
			ArrayList array = GetEnemyInAttackRange(AttackRange.Around);
			foreach(GameObject go in array){
				go.SendMessage("TakeDamage",damageArray[1]+","+proArray[3]+","+proArray[4]);
			}
		} else if(posType == "skill2"){
			ArrayList array = GetEnemyInAttackRange(AttackRange.Around);
			foreach(GameObject go in array){
				go.SendMessage("TakeDamage",damageArray[2]+","+proArray[3]+","+proArray[4]);
			}
		} else if(posType == "skill3"){
			ArrayList array = GetEnemyInAttackRange(AttackRange.Forward);
			foreach(GameObject go in array){
				go.SendMessage("TakeDamage",damageArray[3]+","+proArray[3]+","+proArray[4]);
			}
		}
	}

	void PlaySound(string soundName){
		SoundManager._instance.Play (soundName);
	}

	void ShowPlayerEffect(string effectName) {
		PlayerEffect pe;
		if (effectDict.TryGetValue(effectName, out pe)) {
			pe.Show();
		}
	}

	//Devil Hand Effect 
	void ShowEffectDevilHand(){
		string effectName = "DevilHandMobile";
		PlayerEffect pe;
		effectDict.TryGetValue (effectName, out pe);
		ArrayList array = GetEnemyInAttackRange (AttackRange.Forward);
		foreach (GameObject go in array) {
			RaycastHit hit;
			bool collider = Physics.Raycast(go.transform.position+Vector3.up,Vector3.down,out hit,10f,LayerMask.GetMask("Ground"));
			if(collider){
				GameObject.Instantiate(pe, hit.point, Quaternion.identity);
			}

		}
	}

	//skill1 bird effect(at the end of the skill1,invoke this method)
	void ShowEffectSelfToTarget(string effectName){
		PlayerEffect pe;
		effectDict.TryGetValue (effectName, out pe);
		ArrayList array = GetEnemyInAttackRange (AttackRange.Around);
		foreach (GameObject go in array) {
			//bool collider = Physics.Raycast(go.transform.position+Vector3.up,Vector3.down,out hit,10f,LayerMask.GetMask("Ground"));
			GameObject goEffect = (GameObject.Instantiate(pe) as PlayerEffect).gameObject;
			goEffect.transform.position = transform.position + Vector3.up;
			goEffect.GetComponent<EffectSettings>().Target = go;
		}
	}

	//skill2 fire effect(at the end of the skill,invoke this method)
	void ShowEffectToTarget(string effectName){
		PlayerEffect pe;
		effectDict.TryGetValue (effectName, out pe);
		ArrayList array = GetEnemyInAttackRange (AttackRange.Around);
		foreach (GameObject go in array) {
			RaycastHit hit;
			bool collider = Physics.Raycast(go.transform.position+Vector3.up,Vector3.down,out hit,10f,LayerMask.GetMask("Ground"));
			if(collider){
				GameObject goEffect = (GameObject.Instantiate(pe) as PlayerEffect).gameObject;
				goEffect.transform.position = hit.point;
			}
		}
	}
	
	//get the enemy in the attack range
	//the distance of the attack
	//de dao gongji fanwei zhinei de diren
	ArrayList GetEnemyInAttackRange(AttackRange attackRange){
		ArrayList arrayList = new ArrayList ();
		if (attackRange == AttackRange.Forward) {
			foreach(GameObject go in TranscriptManager._instance.enemyList){
				Vector3 pos = transform.InverseTransformPoint(go.transform.position);
				if(pos.z>-0.1f){
					float distance = Vector3.Distance(Vector3.zero,pos);
					if(distance < distanceAttackForward){
						arrayList.Add(go);
					}
				}
			}
		} else {
			foreach(GameObject go in TranscriptManager._instance.enemyList){
				float distance = Vector3.Distance(transform.position, go.transform.position);
				if(distance < distanceAttackAround){
					arrayList.Add(go);
				}
			}
		}
		return arrayList;
	}

	// Use this for initialization
	void Start () {
		//init the dictionary
		PlayerEffect[] peArray = this.GetComponentsInChildren<PlayerEffect>();
		foreach (PlayerEffect pe in peArray) {
			effectDict.Add(pe.gameObject.name, pe); 
		}
		foreach (PlayerEffect pe in effectArray) {
			effectDict.Add(pe.gameObject.name, pe);
		}
		anim = this.GetComponent<Animator> ();
		//hp bar position
		damageShowPoint = transform.Find ("DamageShowPoint");
		//hp bar
		hudTextGameObject = HpBarManager._instance.GetHudText (damageShowPoint.gameObject);
		hudText = hudTextGameObject.GetComponent<HUDText> ();
		PlayerHpProgressBar._instance.Show (hp);
	}
	
	void TakeDamage(int damage){
		if(hp <= 0){
			return;
		}
		this.hp -= damage;
		//1.show the hit animation
		int random = Random.Range (0, 100);
		if(random < damage){
			anim.SetTrigger("TakeDamage");
		}
		//2.show the blood lose
		hudText.Add ("-"+damage, Color.red, 0.3f);
		//3.blood effect
		BloodScreen._instance.Show ();
		PlayerHpProgressBar._instance.UpdateShow (hp);
		if (hp <= 0) {
			Dead();
		}
	}

	void Dead(){
		anim.SetBool ("Die", true);
		//Destroy (TranscriptManager._instance.player);
	}
}
