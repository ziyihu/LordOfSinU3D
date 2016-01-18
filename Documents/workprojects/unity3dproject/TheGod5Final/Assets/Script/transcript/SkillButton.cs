using UnityEngine;
using System.Collections;

public class SkillButton : MonoBehaviour {

	public PosType posType = PosType.Basic;
	public float coldTime = 4;
	private float coldTimer = 0;	//time left for the skill be useful again
	private UISprite maskSprite;
	private UIButton btn;
//	public UIButton skill1Btn;
//	public UIButton skill2Btn;
//	public UIButton skill3Btn;
//	private UISprite mask1Sprite;
//	private UISprite mask2Sprite;
//	private UISprite mask3Sprite;
//	public GameObject mask1;
//	public GameObject mask2;
//	public GameObject mask3;

	private PlayerAnimation playerAnimation;

	void Start(){
		playerAnimation = TranscriptManager._instance.player.GetComponent<PlayerAnimation> ();
		if(transform.Find("Mask")){
			maskSprite = transform.Find ("Mask").GetComponent<UISprite> ();
		}
		btn = this.GetComponent<UIButton> ();
//		mask1Sprite = transform.parent.Find ("Skill1").GetComponentInChildren<UISprite> ();
//		mask2Sprite = transform.parent.Find ("Skill2").GetComponentInChildren<UISprite> ();
//		mask3Sprite = transform.parent.Find ("Skill3").GetComponentInChildren<UISprite> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (maskSprite == null) {
			return;
		} 
		if (coldTimer > 0) {
			coldTimer -= Time.deltaTime;
//			maskSprite.fillAmount = coldTimer/coldTime;
//			mask1Sprite.fillAmount = coldTimer/coldTime;
//			mask2Sprite.fillAmount = coldTimer/coldTime;
//			mask3Sprite.fillAmount = coldTimer/coldTime;
			if(coldTimer <= 0){
				Enable();
			}
		} else {
			maskSprite.fillAmount = 0;
//			mask1Sprite.fillAmount = 0;
//			mask2Sprite.fillAmount = 0;
//			mask3Sprite.fillAmount = 0;
		}
	}

	void OnPress(bool isPress){
		playerAnimation.OnAttackButtonClick (isPress, posType);
		if (isPress && maskSprite!=null) {
			coldTimer = coldTime;
			Disable();
		}
	}

	void Disable(){
//		skill1Btn.collider.enabled = false;
//		skill2Btn.collider.enabled = false;
//		skill3Btn.collider.enabled = false;
		this.collider.enabled = false;
		btn.SetState (UIButtonColor.State.Normal, true);
//		skill1Btn.SetState (UIButtonColor.State.Normal, true);
//		skill2Btn.SetState (UIButtonColor.State.Normal, true);
//		skill3Btn.SetState (UIButtonColor.State.Normal, true);
	}

	void Enable(){
//		skill1Btn.collider.enabled = true;
//		skill2Btn.collider.enabled = true;
//		skill3Btn.collider.enabled = true;
		this.collider.enabled = true;
//		btn1.collider.enabled = true;
	}
}
