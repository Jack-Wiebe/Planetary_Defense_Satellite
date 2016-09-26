using UnityEngine;
using System.Collections;

[RequireComponent(typeof (Player_Controller))]
public class Player_Input_System : MonoBehaviour {

	private Player_Controller m_Player;
	private bool m_shooting = false;
	private Player_Shooting_System m_Weapon;

	// Use this for initialization
	void Start () {
		m_Player = this.GetComponent<Player_Controller> ();
		m_Weapon = this.GetComponent<Player_Shooting_System> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		float rotation = Input.GetAxis ("Horizontal");
		float fire = Input.GetAxis ("Jump");

		m_Player.Move (-rotation);

		if (fire > 0 && !m_shooting) {
			m_Weapon.ShootOn ();
			m_shooting = true;
		}
		else if (fire == 0) {
			m_Weapon.ShootOff ();
			m_shooting = false;
		}

	}
}
