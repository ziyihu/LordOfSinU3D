using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour {

	public static SoundManager _instance;
	private Dictionary<string,AudioClip> audioDict = new Dictionary<string, AudioClip> ();
	public AudioClip[] audioClipArray;
	public AudioSource audioSource;
	public bool isQuiet = false;

	void Awake(){
		_instance = this;
	}

	// Use this for initialization
	void Start () {
		foreach (AudioClip ac in audioClipArray) {
			audioDict.Add(ac.name,ac);
		}
	}
	
	public void Play(string audioName){
		if (isQuiet) {
			return;
		}
		AudioClip ac;
		if (audioDict.TryGetValue (audioName, out ac)) {
			//Play Sound : Method 1  Not good for the project, consume too many 
			//AudioSource.PlayClipAtPoint(ac,Vector3.zero);

			//Play Sound : Method 2
			this.audioSource.PlayOneShot(ac);
		}
	}

	public void Play(string audioName, AudioSource audioSource){
		if (isQuiet) {
			return;
		}
		AudioClip ac;
		if (audioDict.TryGetValue (audioName, out ac)) {
			audioSource.PlayOneShot(ac);
		}
	}
}
