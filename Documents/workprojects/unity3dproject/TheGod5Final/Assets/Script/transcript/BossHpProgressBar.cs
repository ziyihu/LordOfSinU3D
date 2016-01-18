using UnityEngine;
using System.Collections;

public class BossHpProgressBar : MonoBehaviour {

	public static BossHpProgressBar _instance;
	private UISlider hpSlider;
	private UILabel hpLabel;
	private float maxHp;

	void Awake(){
		_instance = this;
	}

	// Use this for initialization
	void Start () {
		hpSlider = transform.Find("Sprite").GetComponent<UISlider> ();
		hpLabel = transform.Find ("HpLabel").GetComponent<UILabel> ();
		this.gameObject.SetActive (true);
	}

	public void Show(float maxHp){
		this.maxHp = maxHp;
		this.gameObject.SetActive (true);
		UpdateShow (maxHp);
	}

	public void UpdateShow(float currentHp){
		if (currentHp <= 0) {
			currentHp = 0;
		}
		hpSlider.value = (float)currentHp / maxHp;
		hpLabel.text = currentHp + "/" + maxHp;
	}

//	public void Hide(){
//		this.gameObject.SetActive (false);
//	}
}
