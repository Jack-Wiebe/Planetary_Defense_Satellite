using UnityEngine;
using System.Collections;

public enum P_TYPE
{
	BULLET,
	LAZER,
	MISSILE
}

abstract public class Projectile : MonoBehaviour {

	protected Vector3 m_dir;
	[SerializeField] protected float m_bulletSpeed;
	[SerializeField] protected float m_bulletLife;
	[SerializeField] protected GameObject m_player;
	[SerializeField] protected int m_dmg = 1;
	[SerializeField] protected float m_spread;
	//[SerializeField] protected float m_rate;
	[SerializeField] protected P_TYPE m_type;
	protected Projectile b_projectile;
	protected Projectile l_projectile;
	protected Projectile m_projectile;


	// Use this for initialization
	void Start () {
	
	}

	protected virtual void Awake(){
		b_projectile = GetComponent<Bullet> ();
		l_projectile = GetComponent<Lazer> ();
		m_projectile = GetComponent<Missile> ();
		m_player = GameObject.FindGameObjectWithTag ("Player");
	}

	// Update is called once per frame
	protected virtual void Update () {
		this.transform.position += this.transform.up * (m_bulletSpeed * Time.deltaTime);
	}

	public virtual void findPlayer(GameObject player)
	{
		m_player = player;
	}

	protected virtual IEnumerator DestroyMe()
	{
		yield return new WaitForSeconds (m_bulletLife);
		this.gameObject.SetActive (false);

	}

	protected virtual void OnTriggerEnter(Collider target)
	{
		if (target.CompareTag("Enemy")) {
			Bullet_Hit (m_player, target.gameObject, m_dmg);
		}
	}
		
	//Run Everytime the Bullet is enabled
	protected virtual void OnEnable () {
		StartCoroutine (DestroyMe());
	}
	//Run Everytime the Bullet is disabled
	protected virtual void OnDisable () {
		StopAllCoroutines ();
	}

	public virtual void Change_Speed(float speed)
	{
		m_bulletSpeed = speed;
	}

	public virtual void Change_Spread(float spread)
	{
		m_spread = spread;
	}

	public virtual void Change_Rate(float rate)
	{
		m_player.GetComponent<Player_Shooting_System> ().Change_Rate (rate);
	}

	public virtual void Change_Damage(int dmg)
	{
		m_dmg = dmg;
	}

	public virtual Projectile Change_Type(P_TYPE type)
	{
		l_projectile.m_type = type;
		m_projectile.m_type = type;
		b_projectile.m_type = type;

		switch (type) {
		case P_TYPE.LAZER:
			l_projectile.enabled = true;
			m_projectile.enabled = false;
			b_projectile.enabled = false;
			return l_projectile;

		case P_TYPE.MISSILE:
			l_projectile.enabled = false;
			m_projectile.enabled = true;
			b_projectile.enabled = false;
			return m_projectile;
		
		case P_TYPE.BULLET:
			l_projectile.enabled = false;
			m_projectile.enabled = false;
			b_projectile.enabled = true;
			return b_projectile;

		default:
			return null;
		}
	}

	public virtual void Bullet_Hit (GameObject instigator, GameObject target, float dmg)
	{

	}
}
