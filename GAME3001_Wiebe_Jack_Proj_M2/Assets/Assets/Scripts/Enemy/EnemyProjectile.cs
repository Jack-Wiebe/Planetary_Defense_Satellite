using UnityEngine;
using System.Collections;

public class EnemyProjectile : Projectile {

	public override void OnTriggerEnter2D(Collider2D target)
	{
		//Debug.Log (target);
		if (target.CompareTag("Player")) {
			Bullet_Hit (m_player, target.gameObject, m_dmg);
		}

		//Debug.Log (target);
		if (target.CompareTag("Shield")) {
			this.gameObject.SetActive (false);
		}

	}

	public override void Bullet_Hit (GameObject instigator, GameObject target, float dmg)
	{
		if (instigator == null) {
			Debug.Log ("No instigator found");
			return;
		}
		//
		//spawn hit explosion[object pool]
		//
		if (target.CompareTag ("Player")) {
			Player_Stats.instance.ChangeHealth (m_dmg);
			this.gameObject.SetActive (false);
		}

	}

}
