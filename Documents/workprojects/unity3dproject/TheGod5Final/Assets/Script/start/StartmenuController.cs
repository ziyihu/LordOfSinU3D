using UnityEngine;
using System.Collections;

public class StartmenuController : MonoBehaviour {

	public static StartmenuController _instance;

	public TweenScale startpanelTween;	//start page
	public TweenScale loginpanelTween;	//login page
	public TweenScale registerpanelTween; //register page
	public TweenScale serverpanelTween;		//server page
	//public TweenScale charactershowselectTween; //charactershowselect page

	public TweenPosition characterShowTween;	//charactershowselect
	public TweenPosition startpanelTweenPos;	
	public TweenPosition characterselectTween;	//characterselect
	
	public UIInput usernameInputLogin;
	public UIInput passwrodInputLogin;

	public UILabel usernameLabelStart;
	public UILabel serverLabelStart;

	public static string username;
	public static string password;

	public UIInput usernameInputRegister;
	public UIInput passwordInputRegister;
	public UIInput repasswordInputRegister;

	public UILabel serverLabelWindia;
	public UILabel serverLabelScania;
	public UILabel serverLabelBroa;
	public UILabel serverLabelKhaini;
	public UILabel serverLabelZenith;
	public UILabel serverLabelGalicia;
	public UILabel serverLabelTop;

	//select the character
	public GameObject[] characterArray;
	public GameObject[] characterSelectedArray;
	//current character
	private GameObject characterSelected;

	public UIInput characternameInput;
	//change character position
	public Transform characterSelectedParent;

	public UILabel nameLabelCharacterselect;
	public UILabel levelLabelCharacterselect;

	void Awake(){
		_instance = this;
	}

	/*start page button click*/
	public void OnUsernameClick() {
		//enter the username and login
		startpanelTween.PlayForward ();
		StartCoroutine (HidePanel (startpanelTween.gameObject));
		loginpanelTween.gameObject.SetActive (true);
		loginpanelTween.PlayForward ();
	}

	public void OnServerClick() {
		//select the serve
		//hide start menu
		startpanelTween.PlayForward ();
		StartCoroutine (HidePanel (startpanelTween.gameObject));
		serverpanelTween.gameObject.SetActive (true);
		serverpanelTween.PlayForward ();
	}

	public void OnEnterGameClick() {
		//1.connect server, ensure the username and password is correct
		//TODO

		//2.go to the role select scene
		//TODO
		startpanelTweenPos.PlayForward ();
		HidePanel (startpanelTweenPos.gameObject);
		characterselectTween.gameObject.SetActive (true);
		characterselectTween.PlayForward ();
	}

	//hide the scene
	IEnumerator HidePanel(GameObject go) {
		yield return new WaitForSeconds(0.8f);
		go.SetActive (false);
	}


	/*login page button click*/
	//login page button click
	public void OnLoginClick(){
		//get the username and password, store
		username = usernameInputLogin.value;
		password = passwrodInputLogin.value;
		//return to the start page
		loginpanelTween.PlayReverse ();
		StartCoroutine (HidePanel (loginpanelTween.gameObject));
		startpanelTween.gameObject.SetActive (true);
		startpanelTween.PlayReverse ();

		usernameLabelStart.text = username;
	}
	
	public void OnRegisterShowClick(){
		//hide the current page, show the register page
		loginpanelTween.PlayReverse ();
		StartCoroutine (HidePanel (loginpanelTween.gameObject));
		registerpanelTween.gameObject.SetActive (true);
		registerpanelTween.PlayForward ();
	}

	public void OnLoginCloseClick(){
		//return to the start page
		loginpanelTween.PlayReverse ();
		StartCoroutine (HidePanel (loginpanelTween.gameObject));
		startpanelTween.gameObject.SetActive (true);
		startpanelTween.PlayReverse ();
	}


	/*register page button click*/
	public void OnRegisterCancelClick(){
		//hide register page
		registerpanelTween.PlayReverse ();
		StartCoroutine (HidePanel (registerpanelTween.gameObject));
		//show login page
		loginpanelTween.gameObject.SetActive (true);
		loginpanelTween.PlayForward ();
	}

	public void OnRegisterCloseClick(){
		//hide register page
		registerpanelTween.PlayReverse ();
		StartCoroutine (HidePanel (registerpanelTween.gameObject));
		//show login page
		loginpanelTween.gameObject.SetActive (true);
		loginpanelTween.PlayForward ();
	}

	public void OnRegisterAndLoginClick(){
		//connect to the serve to ensure the username
		//TODO

		//connect error
		//TODO

		//connect success
		//save username and password
		username = usernameInputRegister.value;
		password = passwordInputRegister.value;
		//return to the start page
		//hide register page
		registerpanelTween.PlayReverse ();
		StartCoroutine (HidePanel (registerpanelTween.gameObject));
		//show start page
		startpanelTween.gameObject.SetActive (true);
		startpanelTween.PlayReverse ();

		usernameLabelStart.text = username;
	}


	/*server page button click*/
	public void OnServerWindiaClick () {
		serverLabelTop.text = serverLabelWindia.text;
		serverLabelStart.text = serverLabelWindia.text;
	}

	public void OnServerScaniaClick () {
		serverLabelTop.text = serverLabelScania.text;
		serverLabelStart.text = serverLabelScania.text;
	}

	public void OnServerBroaClick () {
		serverLabelTop.text = serverLabelBroa.text;
		serverLabelStart.text = serverLabelBroa.text;
	}

	public void OnServerKhainiClick () {
		serverLabelTop.text = serverLabelKhaini.text;
		serverLabelStart.text = serverLabelKhaini.text;
	}

	public void OnServerGaliciaClick () {
		serverLabelTop.text = serverLabelGalicia.text;
		serverLabelStart.text = serverLabelGalicia.text;
	}

	public void OnServerZenithClick () {
		serverLabelTop.text = serverLabelZenith.text;
		serverLabelStart.text = serverLabelZenith.text;
	}
	
	public void OnServerCancelClick () {
		//hide server page
		serverpanelTween.PlayReverse ();
		StartCoroutine (HidePanel (serverpanelTween.gameObject));
		//show start page
		startpanelTween.gameObject.SetActive (true);
		startpanelTween.PlayReverse ();
	}


	/*characterselect page button click*/
	public void OnCharacterselectCancelClick(){
		//hide characterselect page
		characterselectTween.PlayReverse ();
		StartCoroutine (HidePanel(characterselectTween.gameObject));
		//show start page
		startpanelTweenPos.gameObject.SetActive (true);
		startpanelTweenPos.PlayReverse ();
	}

	//tap the change character button
	public void OnCharacterselectChangeClick(){
		//hide characterselect page
		characterselectTween.PlayReverse ();
		StartCoroutine (HidePanel(characterselectTween.gameObject));
		//show charactershowselect page
		characterShowTween.gameObject.SetActive (true);
		characterShowTween.PlayForward ();
	}


	/*charactershowselect page button click*/
	public void OnCharactershowselectCancelClick(){
		//hide characterselect page
		characterShowTween.PlayReverse ();
		StartCoroutine (HidePanel(characterShowTween.gameObject));
		//show character page
		characterselectTween.gameObject.SetActive (true);
		characterselectTween.PlayForward ();
	}

	//change character, one girl, one boy
	public void OnCharactershowClick(GameObject go){
		//larger the select character
		if (characterSelected == go) {
			return;
		}
		iTween.ScaleTo (go, new Vector3 (1.2f, 1.2f, 1.2f), 0.5f);
		//let the other character be the original size
		foreach (GameObject character in characterArray) {
			if(character != go){
				iTween.ScaleTo (character, new Vector3 (1f, 1f, 1f), 0.5f);	
			} 
		} 
		characterSelected = go;
	}

	//button confirm
	public void OnCharactershowButtonConfirmClick(){
		//1. whether the name is correct
		//TODO
		//2. whether select the character
		//TODO
		int index = -1;
		for (int i=0; i<characterArray.Length; i++) {
			if(characterSelected == characterArray[i]){
				index = i;
				break;
			}
		}
		if (index == -1) {
			return;	
		}
		//GameObject.Destroy(characterSelectedParent.GetComponentInChildren<Animation>().gameObject);
		for(int i = 0;i<characterSelectedParent.childCount;i++)
		{
			GameObject goDelete = characterSelectedParent.GetChild(i).gameObject;
			Destroy(goDelete);
		}
		//create the new character
		GameObject go = GameObject.Instantiate (characterSelectedArray[index], Vector3.zero, Quaternion.identity) as GameObject;
		go.transform.parent = characterSelectedParent;
		go.transform.localPosition = Vector3.zero;
		go.transform.localRotation = Quaternion.identity;
		go.transform.localScale = new Vector3(1f,1f,1f);
		//update the character name and level
		nameLabelCharacterselect.text = characternameInput.value;
		levelLabelCharacterselect.text = "Lv.1";

		OnCharactershowselectCancelClick ();
	}

	public void OnGamePlay(){
		AsyncOperation operation = Application.LoadLevelAsync (1);
		LoadSceneProgressBar._instance.Show (operation);
	}


}
