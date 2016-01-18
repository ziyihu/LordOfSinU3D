using UnityEngine;
using System.Collections;

public class TranscriptMapUI : MonoBehaviour {

	public static TranscriptMapUI _instance;
	private TweenPosition tween;
	private TranscriptMapDialog dialog;
	private UIButton closeButton;

	void Awake(){
		_instance = this;
		tween = this.GetComponent<TweenPosition> ();
		dialog = transform.Find ("TranscriptMapDialog").GetComponent<TranscriptMapDialog> ();
		closeButton = transform.Find ("BtnBack").GetComponent<UIButton> ();

		EventDelegate ed1 = new EventDelegate(this,"OnCloseButtonClick");
		closeButton.onClick.Add (ed1);
	}

	public void Show(){
		tween.PlayForward ();
	}

	public void Hide(){
		tween.PlayReverse ();
	}

	public void OnBtnTranscriptClick(BtnTranscript transcript){
		PlayerInfo info = PlayerInfo._instance;
		if(info.Level>=transcript.needLevel){
			dialog.ShowDialog(transcript);
		} else {
			dialog.ShowWarn();
		}
	}

	public void OnCloseButtonClick(){
		tween.PlayReverse ();
	}

	public void OnEnter(){

	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
