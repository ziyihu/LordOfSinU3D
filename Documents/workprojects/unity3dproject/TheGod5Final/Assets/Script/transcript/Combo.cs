using UnityEngine;
using System.Collections;

public class Combo : MonoBehaviour {

	public static Combo _instance;
	//you need continue the combo in 2 seconds
	public float comboTime = 2;
	private float timer = 0;
	//combo number
	private int comboCount = 0;
	private UILabel numberLabel;

	void Awake(){
		_instance = this;
		numberLabel = transform.Find ("NumberLabel").GetComponent<UILabel> ();
		this.gameObject.SetActive(false);
	}

	void Update(){
		timer -= Time.deltaTime;
		if (timer <= 0) {
			this.gameObject.SetActive(false);
			comboCount = 0;
		}
	}

	//add the combo attack
	public void ComboPlus(){
		this.gameObject.SetActive (true);
		timer = comboTime;
		comboCount++;
		numberLabel.text = comboCount.ToString ();
		transform.localScale = Vector3.one;
		iTween.ScaleTo (this.gameObject, new Vector3 (1.3f, 1.3f, 1.3f), 0.1f);
		iTween.ShakePosition (this.gameObject, new Vector3(0.2f,0.2f,0.2f),0.2f);
	}


}
