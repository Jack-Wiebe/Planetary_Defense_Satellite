using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player_Shooting_System : MonoBehaviour {

	[SerializeField] private float m_fireDelay = 0.5f;
	[SerializeField] private bool m_rightGunFiring;
	[SerializeField] private Transform m_barrel;
	[SerializeField] private GameObject m_bullet;

	[SerializeField] private int m_bulletPoolNum = 100;
	[SerializeField] private List<GameObject> Bullets;
	[SerializeField] private List<Projectile> Projectiles;
	[SerializeField] private float cur_spread;
	[SerializeField] private P_TYPE m_type;

	private int projectilesPerFire = 0;

	private IEnumerator m_coroutine;
	// Use this for initialization
	void Start () {
		m_coroutine = Firing ();
		//create object pool
		Bullets = new List<GameObject> ();
		for (int i = 0; i < m_bulletPoolNum; i++) {
			GameObject obj = (GameObject)Instantiate (m_bullet);
			obj.SetActive (false);
			Projectile pro = obj.GetComponent<Projectile> ();
			Projectiles.Add (pro);
			Bullets.Add (obj);
		}
		Change_P_Type (Player_Stats.instance.Get_Type());
		Set_Stats ();
		//Change_P_Type (Player_Stats.instance);
	}

	public void ShootOn()
	{

		StartCoroutine (m_coroutine);
	}

	public void ShootOff()
	{
		StopCoroutine (m_coroutine);
	}

	public void Change_Rate(float rate)
	{
		m_fireDelay = rate;
	}

	public void Change_Speed(int speed)
	{
		for (int i = 0; i < m_bulletPoolNum; i++) {
			Projectiles [i].Change_Speed (speed);
		}
	}

	public void Change_Spread(int spread)
	{
		for (int i = 0; i < m_bulletPoolNum; i++) {
			Projectiles [i].Change_Spread (spread);
		}
	}

	public void Change_Damage(int dmg)
	{
		for (int i = 0; i < m_bulletPoolNum; i++) {
			Projectiles [i].Change_Damage (dmg);
		}
	}

	public void Change_P_Type(P_TYPE type)
	{
		m_type = type;
		for (int i = 0; i < m_bulletPoolNum; i++) {
			Projectiles [i] = Projectiles [i].Change_Type (type);
		}
		switch (type)
		{
		case(P_TYPE.BULLET):
			cur_spread = Player_Stats.instance.b_spread;
			break;
		case(P_TYPE.LAZER):
			cur_spread = Player_Stats.instance.l_spread;
			break;
		case(P_TYPE.MISSILE):
			cur_spread = Player_Stats.instance.m_spread;
			break;
		}
	}

	public P_TYPE Get_P_Type()
	{
		return m_type;
	}


	public void Set_Stats()
	{
		for (int i = 0; i < m_bulletPoolNum; i++) {
			Projectiles [i].Set_Stats (m_type);
		}
	}

	public IEnumerator Firing()
	{

		//determines what gun is currently firing
		//searches objpool for unused bullet to fire
		while (true) {

			yield return new WaitForSeconds (m_fireDelay);

			for (int i = 0; i < Bullets.Count; i++) {
				//for (int spread = 0; spread < cur_spread; spread++) {

					if (!Bullets [i].activeInHierarchy) {
					
					Bullets [i].transform.position = m_barrel.position;

					Quaternion ang = Quaternion.Euler (0.0f, 0.0f, this.transform.rotation.eulerAngles.z);
					Bullets [i].transform.rotation = ang;

					Bullets [i].SetActive (true);

					//i = Bullets.Count;


					//}
					projectilesPerFire++;
					if (projectilesPerFire >= cur_spread) {
						projectilesPerFire = 0;
						break;
					}
				}
			}

			//Debug.Log (Bullets);

			//Debug.Break ();
			//yield return new WaitForSeconds (_fireDelay);

		}
	}
}
