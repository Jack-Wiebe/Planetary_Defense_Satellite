using UnityEngine;
using System.Collections;

public class Shield_Collision : MonoBehaviour {

	public Player_Shield psRef;
	// Use this for initialization
	void Start () {
	
	}

	void OnTriggerEnter2D(Collider2D hit)
	{
		Debug.Log (hit.gameObject);
		if (hit.CompareTag ("EnemyProjectile")) {
			psRef.Shield_Collison ();

		}
	}

	
	// Update is called once per frame
	void Update () {
	
	}
}
