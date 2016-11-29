using UnityEngine;
using System.Collections;

public class Evasive_Enemy_AI : Basic_Enemy_AI {

	[SerializeField]
	protected float m_evadeRadius;
	[SerializeField]
	protected Vector3 m_evadeVector;
	[SerializeField]
	protected float m_evadeSpeed;

	[SerializeField]
	protected Vector3 m_retreatVector;
	[SerializeField]
	protected float m_retreatSpeed;


	protected float original_moveSpeed;
	[SerializeField]
	protected float speedupDistance;
	public bool canEvade = true;

	[SerializeField]
	protected float m_cooldown;

	protected override void Update ()
	{

		LookAt ();
		EvasiveMove ();
		Retreat ();
		IncrementalMove ();
		FireWeapon ();

	}

	void Awake()
	{
		original_moveSpeed = m_moveSpeed;
	}

	protected void IncrementalMove()
	{
		if(m_hasTarget)
			//rbRef.AddForce (this.transform.forward * m_moveSpeed, ForceMode2D.Force);
			this.transform.Translate (Vector3.up * (m_moveSpeed * Time.deltaTime));

		m_destVec = m_targetVec - this.transform.position;
		m_magnitude = m_destVec.magnitude;

		if (m_magnitude > speedupDistance)
			m_moveSpeed+=0.01f;
		else {
			m_moveSpeed = original_moveSpeed;
		}

		if (m_magnitude <= m_stopDist) {
			m_hasTarget = false;
		} else {
			m_hasTarget = true;
		}
	}

	protected void Retreat()
	{
		m_retreatVector = this.transform.position - target.transform.position;
		m_magnitude = m_retreatVector.magnitude;
		if (m_magnitude < m_stopDist) {
			//Debug.Log ("back it up");
			//m_retreatVector = this.transform.position - target.transform.position;

			//this.transform.Translate (m_retreatVector * (m_retreatSpeed * Time.deltaTime));
			//rbRef.velocity = Vector2.zero;
			//rbRef.AddForce (this.transform.forward * m_retreatSpeed);
			this.transform.Translate (-Vector3.up * (m_retreatSpeed * Time.deltaTime));
		}
	}
	protected void EvasiveMove()
	{
		if(canEvade){
			RaycastHit2D hit;

			hit = Physics2D.CircleCast ((Vector2)this.transform.position, m_evadeRadius, Vector2.zero);
			if (hit.collider != null) {
				if (hit.collider.gameObject.CompareTag ("Projectile")) {
					m_evadeVector = hit.transform.position - this.transform.position;
					//Debug.Log (m_evadeVector);

					m_evadeVector = Vector3.Cross (Vector3.forward, m_evadeVector);
					//Vector2.

					Vector2 localEnemySpace = this.transform.InverseTransformPoint (hit.point);
					//Debug.Log(localEnemySpace);
					//Debug.Log(this.transform.localPosition.x);
					//float test = (hit.transform.position - this.transform.position).sqrMagnitude;
					//Debug.Log (test);
					//hit.point.x >this.transform.localPosition.x
					if (localEnemySpace.x < 0)
						rbRef.AddForce (m_evadeVector * -m_evadeSpeed, ForceMode2D.Impulse);
					else {
						rbRef.AddForce (m_evadeVector * m_evadeSpeed, ForceMode2D.Impulse);
					}
					//this.transform.Translate (m_evadeVector * (m_evadeSpeed * Time.deltaTime));
					canEvade = false;
					StartCoroutine (Cooldown ());
				}

			}
		}
	}

	protected IEnumerator Cooldown()
	{
		while (true) {
			yield return new WaitForSeconds (m_cooldown);
			canEvade = true;
			rbRef.velocity = Vector2.zero;
			rbRef.angularVelocity = 0;
		}
	}

}
