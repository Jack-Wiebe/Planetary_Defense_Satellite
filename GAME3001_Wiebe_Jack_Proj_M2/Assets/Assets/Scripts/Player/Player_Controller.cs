using UnityEngine;
using System.Collections;

public class Player_Controller : MonoBehaviour {

	[SerializeField]private float m_speed = 5.0f;

	void Start()
	{

	}
	// Update is called once per frame
	void Update () {
	
	}

	public void Move(float rot)
	{
		
		this.transform.Rotate (0, 0, rot * m_speed);
	}

}
