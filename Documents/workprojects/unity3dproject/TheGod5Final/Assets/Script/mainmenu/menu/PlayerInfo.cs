using UnityEngine;
using System.Collections;

//inorder to know which feature has been changed
public enum InfoType{
	Name,
	HeadPortrait,
	Level,
	Diamond,
	Coin,
	Energy,
	Toughen,
	All
}

public enum PlayerType{
	Warrior,
	FemaleAssassin
}

public class PlayerInfo : MonoBehaviour {

	public static PlayerInfo _instance;
		
	private string _name;	//name
	private string _headPortait;	//head icon
	private int _level = 1;	//level
	private int _diamond;	//diamond number
	private int _coin;		//coin number
	private int _energy;	//health
	private int _toughen;	//toughen
	private PlayerType _playerType; //playertype

	public float energyTimer = 0;
	public float toughenTimer = 0;


	public delegate void OnPlayerInfoChangedEvent (InfoType type);
	public event OnPlayerInfoChangedEvent OnPlayerInfoChanged;

	public string Name{
		get {
			return _name;
		}
		set{
			_name = value;	
		}
	}

	public string HeadPortait{
		get {
			return _headPortait;
		}
		set{
			_headPortait = value;	
		}
	}

	public int Level{
		get {
			return _level;
		}
		set{
			_level = value;	
		}
	}

	public int Diamond{
		get {
			return _diamond;
		}
		set{
			_diamond = value;	
		}
	}

	public int Coin{
		get {
			return _coin;
		}
		set{
			_coin = value;	
		}
	}

	public int Energy{
		get {
			return _energy;
		}
		set{
			_energy = value;	
		}
	}

	public int Toughen{
		get {
			return _toughen;
		}
		set{
			_toughen = value;	
		}
	}

	public PlayerType PlayerType{
		get {
			return _playerType;
		}
		set {
			_playerType= value;
		}
	}

	void Awake(){
		_instance = this;
	}

	void Start(){
		Init ();
	}


	//init, give the original 
	void Init(){
		this.Coin = 9870;
		this.Diamond = 1234;
		this.Energy = 78;
		this.HeadPortait = "头像底板男性";
		this.Level = 12;
		this.Name = "Ziyi Hu";
		this.Toughen = 34;
		OnPlayerInfoChanged (InfoType.All);
	}

//	public void changeName(string newName){
//		this.Name = newName;
//		OnPlayerInfoChanged (InfoType.Name);
//	}


	//automate recover health and toughen(add 1 point every 1 minute)
	//the speed can be changed by the developer
	void Update() {
		//automate add energy(hp)
		if (this.Energy < 100) {
			energyTimer += Time.deltaTime;
			if(energyTimer > 60){
				Energy += 1;
				energyTimer -= 60;
				OnPlayerInfoChanged(InfoType.Energy);
			}
		} else{
			this.energyTimer = 0;
		}
		//automate add toughen
		if (this.Toughen < 50) {
			toughenTimer += Time.deltaTime;
			if(toughenTimer > 60){
				Toughen += 1;
				toughenTimer -= 60;
				OnPlayerInfoChanged(InfoType.Toughen);
			}
		} else{
			this.toughenTimer = 0;
		}
	}

	public bool GetCoin(int count){
		if (Coin >= count) {
			Coin -= count;
			OnPlayerInfoChanged(InfoType.Coin);
			return true;
		} else {
			return false;
		}
	}

	public bool GetEnergy(int energy){
		if(Energy>energy){
			Energy -= energy;
			OnPlayerInfoChanged(InfoType.Energy);
			return true;
		} else {
			return false;
		}
	}
	
}
