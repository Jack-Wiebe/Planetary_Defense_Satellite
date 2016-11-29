using UnityEngine;
using System.Collections;

public class DestroyMe : MonoBehaviour {

	[SerializeField] protected float m_bulletLife;

	//Run Everytime the Bullet is enabled
	protected virtual void OnEnable () {
		StartCoroutine (DestroyProj());
	}
	//Run Everytime the Bullet is disabled
	protected virtual void OnDisable () {
		StopAllCoroutines ();
	}

	protected virtual IEnumerator DestroyProj()
	{
		yield return new WaitForSeconds (m_bulletLife);
		
		this.gameObject.SetActive (false);

	}
}
