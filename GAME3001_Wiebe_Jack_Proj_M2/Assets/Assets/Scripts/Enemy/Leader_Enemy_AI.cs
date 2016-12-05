using UnityEngine;
using System.Collections;

public class Leader_Enemy_AI : Basic_Enemy_AI {

	public GameObject FollowerOne;
	public GameObject FollowerTwo;

	public GameObject Missile;

	public int missileCount = 0;
	public int missileTime = 100;


	protected override void Update ()
	{

		LookAt ();
		BasicMove ();
		FireWeapon ();
		FireMissile ();

	}

	public void FireMissile()
	{
		missileCount++;
		if (missileCount >= missileTime) {
			
			Instantiate (Missile, this.transform.position, Quaternion.LookRotation(-this.transform.forward));
			missileCount = 0;
		}
	}

}
