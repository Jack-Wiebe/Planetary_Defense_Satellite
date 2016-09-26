using UnityEngine;
using System.Collections;

public class Basic_Enemy_AI : MonoBehaviour {

	public GameObject target;
	[SerializeField]private float m_moveSpeed;
	[SerializeField]private float m_rotSpeed;
	[SerializeField]private float m_stopDist;
	private Quaternion m_destRot;
	private Vector3 m_targetVec;
	private Vector3 m_destVec;
	private bool m_hasTarget;


	// Use this for initialization
	void Start () {
		m_targetVec = target.transform.position;
		m_hasTarget = true;
	}
	
	// Update is called once per frame
	void Update () {
		m_destVec = m_targetVec - this.transform.position;
		m_destRot = Quaternion.LookRotation (this.transform.forward,  m_destVec );
		float magnitude = m_destVec.magnitude;
		this.transform.rotation = Quaternion.RotateTowards (this.transform.rotation, m_destRot, m_rotSpeed);
		if(m_hasTarget)
			this.transform.Translate (Vector3.up * (m_moveSpeed * Time.deltaTime));

		if (magnitude <= m_stopDist) {
			m_hasTarget = false;
		}
			
	}
}
