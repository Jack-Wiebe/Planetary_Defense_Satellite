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

		if (Input.GetButtonDown ("Switch")) {
			Debug.Log (m_Weapon.Get_P_Type ());
			switch (m_Weapon.Get_P_Type()) {
			case (P_TYPE.LAZER):
				m_Weapon.Change_P_Type (P_TYPE.MISSILE);
				break;
			case(P_TYPE.MISSILE):
				m_Weapon.Change_P_Type (P_TYPE.BULLET);
				break;
			case(P_TYPE.BULLET):
				m_Weapon.Change_P_Type (P_TYPE.LAZER);
				break;


			}
		}

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
