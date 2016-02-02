using UnityEngine;
using System.Collections;

public class MyNetwork : MonoBehaviour {

	public int connections = 10;
	public int listenPort = 8899;
	public bool useNat = false;
	public string ip = "127.0.0.1";
	public GameObject playerPrefab;

	// Use this for initialization
	void OnGUI() {
		if (Network.peerType == NetworkPeerType.Disconnected) {
			if (GUILayout.Button ("create server")) {
				//create server
				NetworkConnectionError error = Network.InitializeServer (connections, listenPort, useNat); 
				print (error); 
			}
			
			if (GUILayout.Button ("connect server")) {
				//connect server
				NetworkConnectionError error = Network.Connect (ip, listenPort);
				print (error);
			}
		} else if (Network.peerType == NetworkPeerType.Server) {
			GUILayout.Label ("server created");
		} else if (Network.peerType == NetworkPeerType.Client) {
			GUILayout.Label("clint connected");
		}

	}
	//two method is called on server
	void OnServerInitialized() {
		print ("server initialized");
		//create object in network
		int group = int.Parse (Network.player + ""); //current server index
		Network.Instantiate (playerPrefab, new Vector3(0, 10, 0), Quaternion.identity, group);
	}

	void OnPlayerConnected(NetworkPlayer player) {
		print ("one player connected. Index:" + player);
	}

	void OnConnectedToServer() {
		print ("connect to server success");
		int group = int.Parse (Network.player + ""); //current server index
		Network.Instantiate (playerPrefab, new Vector3(0, 10, 0), Quaternion.identity, group);
	}
	//network view  cooprate attribute component
	//network view modify component which is created as main
}
