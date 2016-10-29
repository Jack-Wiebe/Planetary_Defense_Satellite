using UnityEngine;
using System.Collections;

public class Gravity_Field : MonoBehaviour {

	public float orbitSpeed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.Rotate (Vector3.forward * orbitSpeed *Time.deltaTime);
	}

	void OnTriggerEnter2D(Collider2D target)
	{
		if(target.CompareTag("Enemy"))
		{
			target.gameObject.transform.parent = this.transform;
		}

	}

}
