using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public TweenScale knapsackTween;
	public TweenPosition skillTween;
	public TweenPosition contactTween;


	//show the knapsack
	public void OnKnapsackShowButtonClick(){
		knapsackTween.gameObject.SetActive (true);
		knapsackTween.PlayForward ();
	}

	//hide the knapsack
	public void OnKnapsackCancelButtonClick(){
		knapsackTween.PlayReverse ();
		HidePanel (knapsackTween.gameObject);
	}

	//show the contact
	public void OnContactShowButtonClick(){
		contactTween.gameObject.SetActive (true);
		contactTween.PlayForward ();
	}
	
	//hide the contact
	public void OnContactCancelButtonClick(){
		contactTween.PlayReverse ();
		HidePanel (contactTween.gameObject);
	}



	//show the skill
	public void OnSkillShowButtonClick(){
		skillTween.gameObject.SetActive (true);
		skillTween.PlayForward ();
	}

	//hide the skill
	public void OnSkillCancelButtonClick(){
		skillTween.PlayReverse ();
		HidePanel (skillTween.gameObject);
	}

	//hide the scene
	IEnumerator HidePanel(GameObject go) {
		yield return new WaitForSeconds(0.8f);
		go.SetActive (false);
	}

	public void OnCombatButtonClick(){
		AsyncOperation operation = Application.LoadLevelAsync (2);
		LoadSceneProgressBar._instance.Show (operation);
	}


}
