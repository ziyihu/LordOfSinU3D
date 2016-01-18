using UnityEngine;
using System.Collections;

public class BtnTranscript : MonoBehaviour {

	public int id;
	public int needLevel;
	public string sceneName;
	public string des = "This place is horrible, you dare try?";


	public void OnClick(){
		transform.parent.SendMessage ("OnBtnTranscriptClick", this);
	}
}
