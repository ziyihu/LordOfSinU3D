using UnityEngine;
using System.Collections;

public class TranscriptMapDialog : MonoBehaviour {

	private UILabel desLabel;
	private UIButton enterButton;
	private UIButton closeButton;
	private TweenScale tween;

	// Use this for initialization
	void Start () {
		desLabel = transform.Find ("Sprite/DesLabel").GetComponent<UILabel> ();
		enterButton = transform.Find ("BtnEnter").GetComponent<UIButton> ();
		closeButton = transform.Find ("BtnClose").GetComponent<UIButton> ();
		tween = this.GetComponent<TweenScale> ();

		EventDelegate ed1 = new EventDelegate(this,"OnEnter");
		enterButton.onClick.Add (ed1);

		EventDelegate ed2 = new EventDelegate(this,"OnClose");
		closeButton.onClick.Add (ed2);
	}

	public void ShowWarn(){
		enterButton.enabled = false;
		desLabel.text = "Current level too low, not able to go in the dargon";
		tween.PlayForward ();
	}

	public void ShowDialog(BtnTranscript transcript){
		enterButton.enabled = true;
		desLabel.text = transcript.des;
		tween.PlayForward ();
	}

	void OnEnter(){
		transform.parent.SendMessage("OnEnter");
	}

	void OnClose(){
		tween.PlayReverse ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
