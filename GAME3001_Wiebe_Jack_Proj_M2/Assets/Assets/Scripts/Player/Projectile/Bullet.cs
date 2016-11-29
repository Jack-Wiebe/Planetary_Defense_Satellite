using UnityEngine;
using System.Collections;

public class Bullet : Projectile {

	public bool shield = true;
	public SpriteRenderer shield_sprite;

	virtual protected void OnDisable()
	{
		shield_sprite.enabled = false;
		shield = false;
	}

	virtual protected void OnEnable()
	{
		shield_sprite.enabled = true;
		shield = true;
	}

	public override void OnTriggerEnter2D(Collider2D target)
	{
		Debug.Log (m_type);
		if (target.CompareTag("EnemyProjectile") && m_type == P_TYPE.BULLET) {
			//target.gameObject.SetActive (false);
			Debug.Log (shield);
			if (shield) {
				Debug.Log ("turn off shield");
				shield_sprite.enabled = false;
				shield = false;
			} else {
				shield_sprite.enabled = true;
				shield = true;
				//Debug.Break ();
				this.gameObject.SetActive (false);
			}
		}

	}
}