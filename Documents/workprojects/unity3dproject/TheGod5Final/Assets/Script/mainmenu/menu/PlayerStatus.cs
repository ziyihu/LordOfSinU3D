using UnityEngine;
using System.Collections;

public class PlayerStatus : MonoBehaviour {

	public static PlayerStatus _instance;

	private UISprite headSprite;
	private UILabel levelLabel;
	private UILabel nameLabel;
	private UILabel diamondLabel;
	private UILabel coinLabel;

	private TweenPosition tween;
	private UIButton closeButton;

	private UIButton changeNameButton;
	private GameObject changeNameGo;
	private UIInput nameInput;
	private UIButton sureButton;
	private UIButton cancelButton;

	void Awake(){
		_instance = this;
		headSprite = transform.Find ("HeadSprite").GetComponent<UISprite> ();
		levelLabel = transform.Find ("LevelLabel").GetComponent<UILabel> ();
		nameLabel = transform.Find ("NameLabel").GetComponent<UILabel> ();
		diamondLabel = transform.Find ("DiamondLabel/DiamondNumLabel").GetComponent<UILabel> ();
		coinLabel = transform.Find ("CoinLabel/CoinNumLabel").GetComponent<UILabel> ();

		tween = this.GetComponent<TweenPosition> ();
		closeButton = transform.Find ("CancelButton").GetComponent<UIButton> ();

		changeNameButton = transform.Find ("ChangeNameButton").GetComponent<UIButton> ();
		changeNameGo = transform.Find ("ChangeNameBg").gameObject;
		nameInput = transform.Find ("ChangeNameBg/NameInput").GetComponent<UIInput> ();
		sureButton = transform.Find ("ChangeNameBg/SureButton").GetComponent<UIButton> ();
		cancelButton = transform.Find ("ChangeNameBg/CancelButton").GetComponent<UIButton> ();
		changeNameGo.SetActive (false);

		EventDelegate ed = new EventDelegate(this,"OnButtonCloseClick");
		closeButton.onClick.Add (ed);

		EventDelegate ed2 = new EventDelegate (this, "OnButtonChangeNameClick");
		changeNameButton.onClick.Add (ed2);

		EventDelegate ed3 = new EventDelegate (this, "OnButtonSureClick");
		sureButton.onClick.Add (ed3);

		EventDelegate ed4 = new EventDelegate (this, "OnButtonCancelClick");
		cancelButton.onClick.Add (ed4);


		PlayerInfo._instance.OnPlayerInfoChanged += this.OnPlayerInfoChanged;
	}

	void OnDestory(){
		PlayerInfo._instance.OnPlayerInfoChanged -= this.OnPlayerInfoChanged;
	}

	void OnPlayerInfoChanged(InfoType type){
		if (type == InfoType.All || type == InfoType.HeadPortrait || type == InfoType.Level || type == InfoType.Name || type == InfoType.Coin || type == InfoType.Diamond) {
			UpdateShow();		
		}
	}

	void UpdateShow(){
		PlayerInfo info = PlayerInfo._instance;
		headSprite.spriteName = info.HeadPortait;
		levelLabel.text = info.Level.ToString ();
		nameLabel.text = info.Name.ToString ();
		coinLabel.text = info.Coin.ToString ();
		diamondLabel.text = info.Diamond.ToString ();

	}

	public void Show(){
		tween.PlayForward ();
	}

	public void OnButtonCloseClick(){
		tween.PlayReverse ();
	}

	public void OnButtonChangeNameClick(){
		changeNameGo.SetActive (true);
	}

	public void OnButtonSureClick(){
		//find out whether the name is correct
		//TODO
//		PlayerInfo info = PlayerInfo._instance;
//		info.changeName (nameInput.value);
		changeNameGo.SetActive (false);
	}

	public void OnButtonCancelClick(){
		changeNameGo.SetActive (false);
	}
}
