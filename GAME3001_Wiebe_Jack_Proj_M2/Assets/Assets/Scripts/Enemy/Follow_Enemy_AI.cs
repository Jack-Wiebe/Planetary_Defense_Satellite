using UnityEngine;
using System.Collections;

public class Follow_Enemy_AI : Basic_Enemy_AI {

	public GameObject Leader;
	public GameObject OtherFollower;
	public bool hasLeader;
	public float followDistance;
	public float seperationSpeed;
	public float attractionSpeed;
	public float retreatSpeed;

	override protected void Start()
	{
		base.Start ();
		m_targetVec = target.transform.position;
		this.transform.parent = Leader.transform.parent;
		hasLeader = true;
	}

	override protected void Update()
	{
		LookAt ();
		Follow ();
		Retreat ();
		FireWeapon ();
	}

	public override void DestroyEnemeny ()
	{
		//spawner.spawnPool.Remove (this.gameObject);

		//drop item
		//
		//spawn explosion[ojectpool]
		//

		Player_Stats.instance.score += 10;
		Destroy(this.gameObject);
	}

	protected void Retreat()
	{
		Vector3 m_retreatVector = this.transform.position - target.transform.position;
		m_magnitude = m_retreatVector.magnitude;
		if (m_magnitude < m_stopDist) {
			Debug.Log ("back it up");
			//m_retreatVector = this.transform.position - target.transform.position;

			//this.transform.Translate (m_retreatVector * (m_retreatSpeed * Time.deltaTime));
			//rbRef.velocity = Vector2.zero;
			//rbRef.AddForce (this.transform.forward * m_retreatSpeed);
			this.transform.Translate (-Vector3.up * (retreatSpeed * Time.deltaTime));
		}
	}

	protected void Follow()
	{
		if(m_hasTarget)
			this.transform.Translate (Vector3.up * (m_moveSpeed * Time.deltaTime));

		if (m_magnitude <= m_stopDist) {
			m_hasTarget = false;
		} else {
			m_hasTarget = true;
		}

		if(Leader != null && OtherFollower != null)
		{
			if(Vector3.Distance(this.transform.position, OtherFollower.transform.position) < followDistance )
			{
				/*Vector3 v= new Vector3(this.transform.position.x - OtherFollower.transform.position.x,
					this.transform.position.y - OtherFollower.transform.position.y,
						this.transform.position.z);
				//v.Normalize();
				this.transform.Translate(v.normalized * Time.deltaTime);
				//this.transform.position.y -= (OtherFollower.transform.position.y);*/
				//Debug.Break ();
				Seperation ();
			}
			if (Vector3.Distance (this.transform.position, Leader.transform.position) > followDistance) {
				m_destVec += ( Leader.transform.position - this.transform.position);
				rbRef.AddForce (m_destVec.normalized * attractionSpeed);
			}
		}
	}

	protected void Seperation()
	{
		m_destVec += (OtherFollower.transform.position - this.transform.position);
		//Vector2 v = new Vector2(OtherFollower.transform.position.x - this.transform.position.x, OtherFollower.transform.position.y - this.transform.position.y);
		//v.x +=  OtherFollower.transform.position.x - this.transform.position.x;
		//v.y +=  OtherFollower.transform.position.y - this.transform.position.y;
		rbRef.AddForce (-m_destVec.normalized * seperationSpeed);
	}
}
