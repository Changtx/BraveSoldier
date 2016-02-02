using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	public float speed = 800;
	public GameObject[] bulletHoles;
	public float damage = 10;

	// Use this for initialization
	void Start () {
		Destroy (this.gameObject, 3);
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 orgPos = transform.position;
		transform.Translate (Vector3.forward * speed * Time.deltaTime);
		Vector3 direction = transform.position - orgPos;
		float length = (transform.position - orgPos).magnitude; //tracing length

		RaycastHit hitInfo;
		bool isCollider = Physics.Raycast (orgPos, direction, out hitInfo, length); 

		if (isCollider) {
			if (hitInfo.collider.tag == "Player") {
				Player player = hitInfo.collider.GetComponent<Player>();
				if (player.health > 0) {
					player.TakeDamage(damage);
				}
			} else {
				//tracing through something
				int index = Random.Range(0,2);
				GameObject bulletHolePrefab = bulletHoles[index];
				Vector3 pos = hitInfo.point; //trace collider point
				GameObject go = GameObject.Instantiate(bulletHolePrefab,pos, Quaternion.identity) as GameObject; 
				//hitInfo.normal--> hit point normal
				go.transform.LookAt(hitInfo.point - hitInfo.normal);
				go.transform.Translate(Vector3.back * 0.01f);
			}
			Destroy(this.gameObject);
		}
	}
}
