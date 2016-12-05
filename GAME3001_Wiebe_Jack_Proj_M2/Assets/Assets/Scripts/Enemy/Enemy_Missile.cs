using UnityEngine;
using System.Collections;

public class Enemy_Missile : EnemyProjectile {


	[SerializeField] private GameObject m_target;
	[SerializeField]private float m_rotSpeed;
	private Quaternion m_destRot;
	private Vector3 m_targetVec;
	private Vector3 m_destVec;
	private bool m_hasTarget;

	override protected void FixedUpdate () {
		base.FixedUpdate ();
		SeekTarget ();
		Orient ();
	}

	protected void SeekTarget()
	{
		if(m_target != null) 
			m_targetVec = m_target.transform.position;
	}

	protected void Orient()
	{


		if (m_target != null) {
			m_destVec = m_targetVec - this.transform.position;
			m_destRot = Quaternion.LookRotation (this.transform.forward, m_destVec);
			this.transform.rotation = Quaternion.RotateTowards (this.transform.rotation, m_destRot, m_rotSpeed);
		}
	}
}
