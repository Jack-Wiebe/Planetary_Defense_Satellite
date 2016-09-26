using UnityEngine;
using System.Collections;

public class Bullet : Projectile {

	override public void Bullet_Hit(GameObject instigator, GameObject target, float dmg)
	{

		if (instigator == null)
			Debug.Log ("No instigator found");

	}

}