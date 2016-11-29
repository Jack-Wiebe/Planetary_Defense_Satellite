using UnityEngine;
using System.Collections;

public class Follow_Enemy_AI_Rework : Basic_Enemy_AI {

	private Vector3 followTarget;
	public GameObject Leader;
	public GameObject OtherFollower;
	public bool hasLeader;

	public float FOLLOW_DISTANCE;

	// Use this for initialization
	override protected void Start()
	{
		base.Start ();

		//followTarget = -Leader.GetComponent<Rigidbody2D> ().velocity ;
		followTarget = Leader.transform.forward.normalized * FOLLOW_DISTANCE;
		m_targetVec = Leader.transform.position - followTarget;
		//m_targetVec = target.transform.position;
		this.transform.parent = Leader.transform.parent;
		hasLeader = true;
	}
	
	// Update is called once per frame
	void Update () {

		this.transform.position = m_targetVec;
		Debug.Break ();

	}
}
