 using UnityEngine;
using System.Collections;

public class PlayerAnimation : MonoBehaviour {
	private Player player;
	private CharacterController characterController;
	private bool havePlayDieAnimation = false; 

	// Use this for initialization
	void Start () {
		characterController = this.GetComponent<CharacterController>();
		player = this.GetComponent<Player> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (player.health > 0) {
			if (characterController.isGrounded == false) {
				//falling animation
				//			PlayState ("soldierFalling");
				GetComponent<NetworkView> ().RPC ("PlayState", RPCMode.All, "soldierFalling");  //use playState in every clinet
			} else {
				float h = Input.GetAxis ("Horizontal");
				float v = Input.GetAxis ("Vertical");
				if (Mathf.Abs (h) > 0.1f || Mathf.Abs (v) > 0.05f) {
					//				PlayState("soldierWalk");
					GetComponent<NetworkView> ().RPC ("PlayState", RPCMode.All, "soldierWalk");
				} else {
					//				PlayState("soldierIdle");
					GetComponent<NetworkView> ().RPC ("PlayState", RPCMode.All, "soldierIdle");
				}
			} 
		} else {
			if (havePlayDieAnimation == false) {
				GetComponent<NetworkView> ().RPC ("PlayState", RPCMode.All, "soldierDieFront");
				havePlayDieAnimation = true;
			}
		}
	}


	[RPC]
	public void PlayState(string animName) {
		//GetComponent<Animation> ().Play (animName);   //terminate all other animation, play current
		GetComponent<Animation> () .CrossFade (animName, 0.2f);	//0.2s time buffer to play next
	}
}
