using UnityEngine;
using System.Collections;

public class PlayerBar : MonoBehaviour {

	private UISprite headSprite;
	private UILabel nameLabel;
	private UILabel levelLabel;
	private UISlider energySlider;
	private UISlider toughenSlider;
	private UILabel energyLabel;
	private UILabel toughenLabel;
	private UIButton energyPlusButton;
	private UIButton toughenPlusButton;

	private UIButton headButton;

	void Awake(){
		headSprite = transform.Find ("HeadSprite").GetComponent<UISprite> ();
		nameLabel = transform.Find ("NameLabel").GetComponent<UILabel> ();
		levelLabel = transform.Find ("LevelLabel").GetComponent<UILabel> ();
		energySlider = transform.Find ("EnergyProgressBar").GetComponent<UISlider> ();
		toughenSlider = transform.Find ("ToughenProgressBar").GetComponent<UISlider> ();
		energyLabel = transform.Find ("EnergyProgressBar/EnergyLabel").GetComponent<UILabel> ();
		toughenLabel = transform.Find ("ToughenProgressBar/ToughenLabel").GetComponent<UILabel> ();
		energyPlusButton = transform.Find ("EnergyPlusButton").GetComponent<UIButton> ();
		toughenPlusButton = transform.Find ("ToughenPlusButton").GetComponent<UIButton> ();

		headButton = transform.Find ("HeadButton").GetComponent<UIButton> ();

		PlayerInfo._instance.OnPlayerInfoChanged += this.OnPlayerInfoChanged;
		EventDelegate ed = new EventDelegate (this, "OnHeadButtonClick");
		headButton.onClick.Add (ed);
	}

//	void Start(){
//		PlayerInfo._instance.OnPlayerInfoChanged += this.OnPlayerInfoChanged;
//	}

	void OnDestroy(){
		PlayerInfo._instance.OnPlayerInfoChanged -= this.OnPlayerInfoChanged;
	}

	void OnPlayerInfoChanged(InfoType type){
		if (type == InfoType.All||type == InfoType.Name || type == InfoType.HeadPortrait || type == InfoType.Level || type == InfoType.Energy || type == InfoType.Toughen) {
			UpdateShow();
		}
	}

	void Update(){
		UpdateEnergyAndToughenShow ();
	}

	void UpdateShow(){
		PlayerInfo info = PlayerInfo._instance;
		headSprite.spriteName = info.HeadPortait;
		levelLabel.text = info.Level.ToString ();
		nameLabel.text = info.Name.ToString ();
		energySlider.value = info.Energy / 100f;
		energyLabel.text = info.Energy + "/100";
		toughenSlider.value = info.Toughen / 50f;
		toughenLabel.text = info.Toughen + "/50";
	}

	void UpdateEnergyAndToughenShow (){
		PlayerInfo info = PlayerInfo._instance;
		energyLabel.text = info.Energy + "/100";
		toughenLabel.text = info.Toughen + "/50";
	}

	public void OnHeadButtonClick(){
		PlayerStatus._instance.Show ();
	}

}
