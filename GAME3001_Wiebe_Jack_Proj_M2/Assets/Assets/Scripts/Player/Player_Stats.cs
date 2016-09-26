using UnityEngine;
using System.Collections;

public class Player_Stats : MonoBehaviour {

	public float b_speed;
	public float b_bulletSpeed;
	public float b_bulletLife;
	public int b_dmg;
	public float b_spread;

	public float m_speed;
	public float m_bulletSpeed;
	public float m_bulletLife;
	public int m_dmg;
	public float m_spread;

	public float l_speed;
	public float l_bulletSpeed;
	public float l_bulletLife;
	public int l_dmg;
	public float l_spread;

	[SerializeField]private int m_health;
	[SerializeField]private P_TYPE m_type;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
