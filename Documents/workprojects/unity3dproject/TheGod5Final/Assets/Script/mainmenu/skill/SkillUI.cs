using UnityEngine;
using System.Collections;

public class SkillUI : MonoBehaviour {

	public static SkillUI _instance;

	private UILabel skillNameLabel;
	private UILabel skillDesLabel;
	private UIButton closeButton;
	private UIButton upgradeButton;
	private UILabel upgradeButtonLabel;
	private TweenPosition tween;

	//current selected skill
	private Skill skill;

	void Awake(){
		_instance = this;
		skillNameLabel = transform.Find("Bg/SkillNameLabel").GetComponent<UILabel>();
		skillDesLabel = transform.Find ("Bg/DesLabel").GetComponent<UILabel> ();
		closeButton = transform.Find ("CloseButton").GetComponent<UIButton> ();
		upgradeButton = transform.Find ("UpgradeButton").GetComponent<UIButton> ();
		upgradeButtonLabel = transform.Find ("UpgradeButton/Label").GetComponent<UILabel> ();
		tween = this.GetComponent<TweenPosition> ();

		skillNameLabel.text = "";
		skillDesLabel.text = "";
		DisableUpgradeButton("select skill");

		EventDelegate ed = new EventDelegate(this,"OnUpgrade");
		upgradeButton.onClick.Add (ed);

		EventDelegate ed1 = new EventDelegate(this,"OnClose");
		closeButton.onClick.Add(ed1);
	}

	void DisableUpgradeButton(string label=""){
		upgradeButton.SetState (UIButton.State.Disabled,true);
		upgradeButton.collider.enabled = false;
		if (label != "") {
			upgradeButtonLabel.text = label;
		}
	}

	void EnableUpgradeButton(string label=""){
		upgradeButton.SetState (UIButton.State.Normal,true);
		upgradeButton.collider.enabled = true;
		if (label != "") {
			upgradeButtonLabel.text = label;
		}
	}

	public void OnSkillClick(Skill skill){
		this.skill = skill;
		PlayerInfo info = PlayerInfo._instance;
		if ((500 * skill.Level) <= info.Coin) {
			if(skill.Level<info.Level){
				EnableUpgradeButton("Upgrade");
			} else {
				DisableUpgradeButton("Max Level");
			}
		} else {
			DisableUpgradeButton("Need more Coin");
		}
		skillNameLabel.text = skill.Name + "   Lv." + skill.Level;
		skillDesLabel.text = "The damage of current skill is: " + skill.Damage + "*" + skill.Level + "=" + (skill.Damage * skill.Level)
						+ "    Next level damage of the skill is: " + (skill.Damage * (skill.Level + 1)) 
						+ "    Upgrade money: " + (500 * skill.Level);

	}

	void OnUpgrade(){
		PlayerInfo info = PlayerInfo._instance;
		if(skill.Level < info.Level){
			int coinNeed = (500 * skill.Level);
			bool isSuccess = info.GetCoin (coinNeed);
			if(isSuccess){
				skill.Level++;
				OnSkillClick(skill);
			} else {
				DisableUpgradeButton("Need more Coin");
			}
		} else {
			DisableUpgradeButton("Max Level");
		}
	}

	public void Show(){
		tween.PlayForward ();
	}

	public void Hide(){
		tween.PlayReverse ();
	}

	void OnClose(){
		tween.PlayReverse ();
	}

}
