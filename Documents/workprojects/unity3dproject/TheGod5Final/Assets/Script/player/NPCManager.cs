using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NPCManager : MonoBehaviour {

	public static NPCManager _instance;
	public GameObject[] npcArray;
	private Dictionary<int, GameObject> npcDict = new Dictionary<int, GameObject>();

	void Awake(){
		_instance = this;
		Init ();
	}

	void Init(){
//		foreach (GameObject go in npcArray) {
//			int id = int.Parse (go.name.Substring (0, 4));
//			npcDict.Add (id,go);
//		}
	}

//	public GameObject GetNpcById(int id){
//		GameObject go = null;
//		npcDict.TryGetValue (id, out go);
//	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
