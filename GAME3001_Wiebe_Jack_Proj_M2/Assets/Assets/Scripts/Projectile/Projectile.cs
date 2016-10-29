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
	[SerializeField] protected GameObject m_player;

	[SerializeField] protected float m_bulletSpeed;
	[SerializeField] protected int m_dmg = 1;
	[SerializeField] protected float m_spread;

	[SerializeField] protected P_TYPE m_type;

	[SerializeField] protected Rigidbody2D m_RBref;
	[SerializeField] protected TrailRenderer m_TRref;
	[SerializeField] protected SpriteRenderer m_SRref;

	protected Projectile b_projectile;
	protected Projectile l_projectile;
	protected Projectile m_projectile;

	[SerializeField]protected Sprite m_sprite;


	// Use this for initialization
	void Start () {
	
	}

	protected virtual void Awake(){

		m_RBref = GetComponent<Rigidbody2D> ();
		m_SRref = GetComponent<SpriteRenderer> ();
		m_TRref = GetComponent<TrailRenderer> ();
		b_projectile = GetComponent<Bullet> ();
		l_projectile = GetComponent<Lazer> ();
		m_projectile = GetComponent<Missile> ();
		m_player = GameObject.FindGameObjectWithTag ("Player");
	}

	// Update is called once per frame
	protected virtual void FixedUpdate () {
		Move ();
		Draw ();
	}

	protected virtual void Draw()
	{
		m_SRref.sprite = m_sprite;
	}

	protected virtual void Move()
	{
		//this.transform.position += this.transform.up * (m_bulletSpeed * Time.deltaTime);
		m_RBref.velocity = this.transform.up * (m_bulletSpeed);
	}

	public virtual void findPlayer(GameObject player)
	{
		m_player = player;
	}



	public virtual void OnTriggerEnter2D(Collider2D target)
	{
		Debug.Log (target);
		if (target.CompareTag("Enemy")) {
			Bullet_Hit (m_player, target.gameObject, m_dmg);
		}
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
		if (instigator == null) {
			Debug.Log ("No instigator found");
			return;
		}
		//
		//spawn hit explosion[object pool]
		//
		target.GetComponent<Basic_Enemy_AI> ().ChangeHealth (dmg);
		this.gameObject.SetActive (false);
	}
}
