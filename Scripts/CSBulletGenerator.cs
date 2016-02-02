using UnityEngine;
using System.Collections;

public class CSBulletGenerator : MonoBehaviour {

	public int shootRate = 10;
	public float timer = 0;
	public CSFlash flash;
	public GameObject bulletPrefab;
	public Camera soldierCamera;

	private bool isFiring = false;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			isFiring = true;
		}
		if (Input.GetMouseButtonUp (0)) {
			isFiring = false;
		}

		if (isFiring) {
			timer += Time.deltaTime;
			if (timer > 1f / shootRate) {
				Shoot();
				timer -= 1f / shootRate;
			}
		}
	}

	void Shoot() {
		flash.Flash (); //bullet flash
		GameObject go = GameObject.Instantiate (bulletPrefab, transform.position, transform.rotation) as GameObject;
		//get view object
		//center of camera tracing though somehing
		Vector3 point = soldierCamera.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 0));
		RaycastHit hitInfo;
		bool isCollider = Physics.Raycast (point, soldierCamera.transform.forward, out hitInfo);

		if (isCollider) {
			go.transform.LookAt (hitInfo.point);
		} else {
			point += soldierCamera.transform.forward * 1000;
			go.transform.LookAt(point);
		}
	}
}
