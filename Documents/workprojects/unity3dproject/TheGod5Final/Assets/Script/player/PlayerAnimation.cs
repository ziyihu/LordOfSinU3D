using UnityEngine;
using System.Collections;

public class PlayerAnimation : MonoBehaviour {

	private Animator anim;
	private PlayerAttack playerAttack;

	void Start(){
		anim = this.GetComponent<Animator> ();
		playerAttack = GetComponent<PlayerAttack> ();
	}

	public void OnAttackButtonClick(bool isPress,PosType posType){
		if (playerAttack.hp <= 0) {
			return;
		}
		if (posType == PosType.Basic) {
			if(isPress){
				anim.SetTrigger("Attack");
			}
		} else {
			if(isPress){
				anim.SetBool("Skill"+(int)posType,true);
			} else {
				anim.SetBool("Skill"+(int)posType,false);
			}
		}
	}


	
	// Update is called once per frame
	void Update () {
	
	}
}
