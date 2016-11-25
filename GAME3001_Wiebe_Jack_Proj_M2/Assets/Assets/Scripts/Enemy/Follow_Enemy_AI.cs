using UnityEngine;
using System.Collections;

public class Follow_Enemy_AI : Basic_Enemy_AI {

	public GameObject Leader;
	public GameObject OtherFollower;
	public bool hasLeader;
	public float followDistance;

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

	protected void Follow()
	{
		if(m_hasTarget)
			this.transform.Translate (Vector3.up * (m_moveSpeed * Time.deltaTime));

		if (m_magnitude <= m_stopDist) {
			m_hasTarget = false;
		}

		if(Leader != null)
		{
			if(Vector3.Distance(this.transform.position, OtherFollower.transform.position) < followDistance )
			{
				Vector3 v= new Vector3(this.transform.position.x - OtherFollower.transform.position.x,
					this.transform.position.y - OtherFollower.transform.position.y,
						this.transform.position.z);
				//v.Normalize();
				this.transform.Translate(v.normalized * Time.deltaTime);
				//this.transform.position.y -= (OtherFollower.transform.position.y);
			}
		}
	}
}
