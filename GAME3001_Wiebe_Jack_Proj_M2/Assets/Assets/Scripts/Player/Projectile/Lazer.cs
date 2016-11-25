using UnityEngine;
using System.Collections;

public class Lazer : Projectile {

	protected void OnEnable () {
		Debug.Log (m_TRref);
		if (m_TRref) {
			m_TRref.Clear();
			m_TRref.enabled = true;
		}
			
	}

	protected void OnDisable()
	{
		if (m_TRref)
			m_TRref.enabled = false;
	}   

}
