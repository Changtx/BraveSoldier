using UnityEngine;
using System.Collections;

public class CSBulletHole : MonoBehaviour {

	public float speed = 0.3f;
	private Renderer rend;
	private float timer = 0;

	// Use this for initialization
	void Start () {
		rend = GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		if (timer > 2) {
			rend.material.color = Color.Lerp (rend.material.color, Color.clear, speed * Time.deltaTime);
		}

		if (timer > 10) {
			Destroy(this.gameObject);
		}
	}
}
