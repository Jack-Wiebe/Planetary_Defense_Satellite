﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Basic_Enemy_AI : MonoBehaviour {

	protected float MAXHEALTH;
	[SerializeField]protected float m_enemyHealth = 100.0f;

	public GameObject target;
	[SerializeField]protected float m_moveSpeed;
	[SerializeField]protected float m_rotSpeed;
	[SerializeField]protected float m_stopDist;
	protected Quaternion m_destRot;
	protected Vector3 m_targetVec;
	protected Vector3 m_destVec;
	[SerializeField]
	protected bool m_hasTarget;
	protected float m_magnitude;

	public Spawner spawner;

	protected bool isDead = false; //this is to prevent adding score multiple times

	public Rigidbody2D rbRef;

	public GameObject newProj;
	[SerializeField]protected int m_poolNum;
	[SerializeField]protected List<GameObject> ObjectPool;
	[SerializeField]protected List<Projectile> Projectiles;

	protected float m_count = 0.0f;
	[SerializeField]protected float m_fireRate;

	protected SpriteRenderer m_spirte;
	protected float m_colorValue;

	// Use this for initialization
	virtual protected void Start () {

		MAXHEALTH = m_enemyHealth;
		m_spirte = this.gameObject.GetComponent<SpriteRenderer> ();
		m_targetVec = target.transform.position;
		m_hasTarget = true;
		rbRef = this.GetComponent<Rigidbody2D> ();

		for (int i = 0; i < m_poolNum; i++) {
			GameObject obj = (GameObject)Instantiate (newProj);
			obj.SetActive (false);
			Projectile pRef = obj.GetComponent<Projectile> ();
			Projectiles.Add (pRef);
			ObjectPool.Add (obj);
		}

	}
	
	// Update is called once per frame
	virtual protected void Update () {

		DisplayDamage ();
		LookAt ();
		BasicMove ();
		FireWeapon ();
			
	}

	virtual protected void DisplayDamage()
	{
		m_colorValue = Mathf.Lerp (0, 1, m_enemyHealth / MAXHEALTH);
		m_spirte.material.color = new Color(1.0f, m_colorValue, m_colorValue, 1.0f);
	}

	virtual protected void LookAt()
	{
		m_destVec = m_targetVec - this.transform.position;
		m_destRot = Quaternion.LookRotation (this.transform.forward,  m_destVec );
		m_magnitude = m_destVec.magnitude;
		this.transform.rotation = Quaternion.RotateTowards (this.transform.rotation, m_destRot, m_rotSpeed);
	}

	virtual protected void BasicMove()
	{
		if(m_hasTarget)
			this.transform.Translate (Vector3.up * (m_moveSpeed * Time.deltaTime));

		m_destVec = m_targetVec - this.transform.position;
		m_magnitude = m_destVec.magnitude;

		if (m_magnitude <= m_stopDist) {
			m_hasTarget = false;
		} else {
			m_hasTarget = true;
		}
	}

	virtual protected void FireWeapon()
	{
		m_count++;
		if (m_magnitude <= m_stopDist) {
			while(m_count > m_fireRate) {
				for (int i = 0; i < ObjectPool.Count; i++) {
					if (!ObjectPool [i].activeInHierarchy) {

						ObjectPool [i].transform.position = this.transform.position;
						ObjectPool [i].transform.rotation = Quaternion.Euler (0.0f, 0.0f, this.transform.rotation.eulerAngles.z);
						ObjectPool [i].SetActive (true);
						m_count = 0.0f;
						break;

					}
				}
			}
		}
	}

	virtual public void ChangeHealth(float dmg)
	{
		m_enemyHealth -= dmg;
		if (m_enemyHealth <= 0 && !isDead)
			DestroyEnemeny ();
	}

	virtual public void DestroyEnemeny ()
	{
		//isDead = true;
		spawner.spawnPool.Remove (this.gameObject);

		//drop item
		bool spawning = false;

		while(!spawning)
		{
			int index = Random.Range (0, spawner.explosionPool.Count);
			if (!spawner.explosionPool [index].activeInHierarchy) {
				spawner.explosionPool [index].transform.position = this.transform.position;
				spawner.explosionPool [index].transform.rotation = Quaternion.Euler (0.0f, 0.0f, this.transform.rotation.eulerAngles.z);
				spawner.explosionPool [index].SetActive (true);
				spawning = true;
				break;
			} else {
				spawning = false;
			}
		}
		Player_Stats.instance.score += 10;
		Destroy(this.gameObject);
	}


}
