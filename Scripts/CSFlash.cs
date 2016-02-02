using UnityEngine;
using System.Collections;

public class CSFlash : MonoBehaviour {

	public Material[] mats;
	private int index = 0;
	public float flashTime = 0.05f; //flash time
	private float flashTimer = 0;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
//		if (Input.GetMouseButtonDown (0)) {
//			Flash ();
//		}
		if (GetComponent<Renderer> ().enabled) {
			flashTimer += Time.deltaTime;
			if (flashTimer > flashTime) {
				GetComponent<Renderer>().enabled = false;
			}                          
		}
	}

	public void Flash() {
		index++;
		index %= 4;
		GetComponent<Renderer> ().enabled = true;
		GetComponent<Renderer> ().material = mats [index];
		flashTimer = 0;
	}
}
