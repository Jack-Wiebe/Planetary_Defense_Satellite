﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player_Stats : MonoBehaviour {

	public static Player_Stats instance = null;     

	public int score = 0;
	public Text scoreBoard;
	public Text healthBoard;

	public float b_speed;
	public float b_rate;
	public float b_bulletLife;
	public int   b_dmg;
	public float b_spread;

	public float m_speed;
	public float m_rate;
	public float m_bulletLife;
	public int   m_dmg;
	public float m_spread;

	public float l_speed;
	public float l_rate;
	public float l_bulletLife;
	public int   l_dmg;
	public float l_spread;

	[SerializeField]private int m_health = 100;
	[SerializeField]private P_TYPE m_type;


	void Awake()
	{
		//Check if instance already exists
		if (instance == null) {

			//if not, set instance to this
			instance = this;

			//If instance already exists and it's not this:
		} else if (instance != this) {

			//Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
			Destroy (gameObject);    
		}
		//Sets this to not be destroyed when reloading scene
		DontDestroyOnLoad (gameObject);
		InitGame ();
	}


	private void InitGame()
	{
		return;
	}

	public void ChangeHealth(int dmg)
	{
		m_health -= dmg;
		if (m_health <= 0)
			EndGame ();
		return;
	}

	public void EndGame()
	{
		//destroy player
		//display ending game message 
	}

	// Use this for initialization
	void Start () {
		scoreBoard = GameObject.Find ("Text").GetComponent<Text>();
		healthBoard = GameObject.Find ("Health").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		scoreBoard.text = string.Concat ("Score: ", score);
		healthBoard.text = string.Concat ("Health: ", m_health);
	}
		
}
