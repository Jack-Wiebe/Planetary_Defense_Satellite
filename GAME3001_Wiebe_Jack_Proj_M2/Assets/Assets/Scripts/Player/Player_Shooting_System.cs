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
	}
	
	// Update is called once per frame
	void Update () {

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

	public void Change_Type(P_TYPE type)
	{
		for (int i = 0; i < m_bulletPoolNum; i++) {
			Projectiles [i].Change_Type (type);
		}
	}


	public IEnumerator Firing()
	{

		//determines what gun is currently firing
		//searches objpool for unused bullet to fire
		while (true) {

			yield return new WaitForSeconds (m_fireDelay);

			for (int i = 0; i < Bullets.Count; i++) {

				if (!Bullets [i].activeInHierarchy) {

					Bullets [i].transform.position = m_barrel.position;

					Quaternion ang = Quaternion.Euler (0.0f, 0.0f, this.transform.rotation.eulerAngles.z);
					Bullets [i].transform.rotation = ang;

					Bullets [i].SetActive (true);

					//i = Bullets.Count;
					break;

				}
			}

			Debug.Log (m_fireDelay);

			//Debug.Break ();
			//yield return new WaitForSeconds (_fireDelay);

		}
	}
}
