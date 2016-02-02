using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public UIInput ipInput; //connect IP
	public int connections = 10; //max connections
	public int listenPort = 8899; //listen port
	public GameObject uiRoot;
	public Transform pos1;
	public Transform pos2;
	public GameObject soldierPrefab;
	public AudioSource bgMenuMusic;
	public AudioSource bgPlayMusic;
	public static GameController _instance;
	public GameObject bgGameObject;
	public GameObject logoGameObject;
	public GameObject menuPanelGameObject;
	public GameObject gameoverGameObject;
	public UILabel gameoverLabel;

	void Awake() {
		_instance = this;
	}

	//create server button
	public void OnButtonCreateServer() {
		Network.InitializeServer (connections, listenPort);
		uiRoot.SetActive (false);
		PlayMusic ();
	}

	private void PlayMusic() {
		bgMenuMusic.Stop ();
		bgPlayMusic.Play ();
	}

	//connect server button 
	public void OnButtonConnectServer() {
		//server initailized 
		string ip = ipInput.value;
		if (ip == "") {
			ip = ipInput.defaultText;
		}
		Network.Connect (ip, listenPort);
		uiRoot.SetActive (false);
		PlayMusic ();
	}


	void OnServerInitialized() {
		//server initailized 
		NetworkPlayer player =  Network.player;
		int group = int.Parse (player + "");
//		GameObject.Instantiate (soldierPrefab, pos1.position, Quaternion.identity);
		GameObject go = Network.Instantiate (soldierPrefab, pos1.position, Quaternion.identity, group) as GameObject;
//		go.GetComponent<Player> ().SetOwnerPlayer (Network.player); // set current server player,locally
		go.GetComponent<NetworkView>().RPC ("SetOwnerPlayer", RPCMode.AllBuffered, Network.player);
		Cursor.visible = false;
	}

	void OnConnectedToServer() {
		NetworkPlayer player =  Network.player;
		int group = int.Parse (player + "");
		//		GameObject.Instantiate (soldierPrefab, pos1.position, Quaternion.identity);
		GameObject go = Network.Instantiate (soldierPrefab, pos2.position, Quaternion.identity, group) as GameObject; 
		go.GetComponent<NetworkView>().RPC ("SetOwnerPlayer", RPCMode.AllBuffered, Network.player);
		Cursor.visible = false;
	}

	public void OnExitButton() {
		Application.Quit ();
	}

	public void ShowGameover(bool isWin) {
		//show gameOver menu
		uiRoot.SetActive (true);
		bgGameObject.SetActive (false);
		logoGameObject.SetActive (false);
		menuPanelGameObject.SetActive (false);
		gameoverGameObject.SetActive (true);
		if (isWin) {
			gameoverLabel.text = "YOU WIN";
		} else {
			gameoverLabel.text = "YOU LOSE";
		}
		Cursor.visible = true;
	}
}
