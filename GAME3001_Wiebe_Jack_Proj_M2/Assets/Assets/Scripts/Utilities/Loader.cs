using UnityEngine;
using System.Collections;

public class Loader : MonoBehaviour {

	public GameObject PlayerStats;          //GameManager prefab to instantiate.


	void Awake ()
	{
		//Check if a GameManager has already been assigned to static variable GameManager.instance or if it's still null
		if (Player_Stats.instance == null) {

			//Instantiate gameManager prefab
			Instantiate (PlayerStats);

		}

		this.GetComponent<Spawner> ().StartRound ();
	}
}
