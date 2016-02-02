using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public NetworkPlayer ownerPlayer; //owner of the current player
	public Camera camera;
	public CSBulletGenerator bulletGenerator;
	public MouseLook_ctx xMouseLook;
	public MouseLook_ctx yMouseLook;
	public float health = 100;

	private CharacterMotor motor;
	private PlayerAnimation playerAnimation;


	// Use this for initialization
	void Start () {
		motor = this.GetComponent<CharacterMotor> ();
		playerAnimation = this.GetComponent<PlayerAnimation> ();

	}
	
	// Update is called once per frame
	void Update () {
		if (health <= 0) {
			bool isWin = false;
			if (ownerPlayer != Network.player) {
				isWin = true;
			}
			GameController._instance.ShowGameover(isWin);
		}
	}

	//player not in owner, lose control
	public void LoseControl() {
		motor = this.GetComponent<CharacterMotor> ();
		playerAnimation = this.GetComponent<PlayerAnimation> ();

		camera.gameObject.SetActive (false); //negative camera
		motor.canControl = false; //negative motion control
		playerAnimation.enabled = false; //negative animation
		xMouseLook.enabled = false; //negative mouse look
		yMouseLook.enabled = false;
//		soilder.GetComponent<MouseLook> ().enabled = false;
//		soilderSpine2.GetComponent<MouseLook> ().enabled = false;
		bulletGenerator.enabled = false; //negative bullet generating
	}

	[RPC]
	public void SetOwnerPlayer(NetworkPlayer player) {
		this.ownerPlayer = player;
		if (Network.player != player) {
			LoseControl();  //current player create by others
		}
	}


	public void TakeDamage(float damage) {
		this.health -= damage;
		GetComponent<NetworkView> ().RPC ("SysncHp", RPCMode.All, health); 
	}

	//sys hp
	[RPC]
	public void SysncHp(float health) {
		this.health = health;
	}
	
}
