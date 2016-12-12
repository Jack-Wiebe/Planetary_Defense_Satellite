using UnityEngine;
using System.Collections;

public class Missile : Projectile {

	[SerializeField] private GameObject m_target;
	[SerializeField] private float m_homingRadius;
	private Quaternion m_destRot;
	private Vector3 m_targetVec;
	private Vector3 m_destVec;
	private bool m_hasTarget;
	[SerializeField]private float m_rotSpeed;

	override protected void FixedUpdate () {
		base.FixedUpdate ();
		SeekTarget ();
		Orient ();
	}


	virtual protected void OnEnable () {
		//Debug.Log (m_TRref);
		if (m_TRref) {
			m_TRref.Clear();
			m_TRref.enabled = true;
		}

	}

	virtual protected void OnDisable()
	{
		if (m_TRref)
			m_TRref.enabled = false;
	}   

	protected void SeekTarget()
	{
		RaycastHit2D hit;

		hit = Physics2D.CircleCast ((Vector2)this.transform.position, m_homingRadius, Vector2.zero);
		if (hit.collider != null) {
			if (hit.collider.gameObject.CompareTag ("Enemy")) {
				m_target = hit.collider.gameObject;
			} else {
				m_target = null;
			}
		}else {
			m_target = null;
		}

		if(m_target != null) 
			m_targetVec = m_target.transform.position;
	}

	protected void Orient()
	{


		if (m_target != null) {
			m_destVec = m_targetVec - this.transform.position;
			m_destRot = Quaternion.LookRotation (this.transform.forward, m_destVec);
			this.transform.rotation = Quaternion.RotateTowards (this.transform.rotation, m_destRot, m_rotSpeed * Player_Stats.instance.missile_level);
		}
	}
		
}
