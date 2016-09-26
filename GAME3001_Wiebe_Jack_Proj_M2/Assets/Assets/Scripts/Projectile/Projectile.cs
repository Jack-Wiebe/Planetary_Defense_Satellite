using UnityEngine;
using System.Collections;

public enum P_TYPE
{
	BULLET,
	BEAM,
	MISSILE
}

abstract public class Projectile : MonoBehaviour {

	protected Vector3 m_dir;
	[SerializeField] protected float m_bulletSpeed;
	[SerializeField] protected float m_bulletLife;
	[SerializeField] protected GameObject m_player;
	[SerializeField] protected float m_dmg = 1.0f;
	[SerializeField] protected float m_spread;
	//[SerializeField] protected float m_rate;
	[SerializeField] protected P_TYPE m_type; 

	// Use this for initialization
	void Start () {
	
	}

	protected virtual void Awake(){
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

	public virtual void Change_Speed(int speed)
	{
		m_bulletSpeed = speed;
	}

	public virtual void Change_Spread(int spread)
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

	public virtual void Change_Type(P_TYPE type)
	{
		m_type = type;
	}

	public virtual void Bullet_Hit (GameObject instigator, GameObject target, float dmg)
	{

	}
}
