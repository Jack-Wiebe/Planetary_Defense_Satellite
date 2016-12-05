using UnityEngine;
using System.Collections;

public class Player_Shield : MonoBehaviour {

	public int hitCount;
	public float rechargeTime;
	public GameObject Shield;


	// Use this for initialization
	void Start () {
		hitCount = 0;
		Shield = GameObject.FindGameObjectWithTag ("Shield");
	}


	public void Shield_Collison()
	{
		hitCount++;
		Shield_Check ();
	}

	private void Shield_Check()
	{
		if (hitCount >= 3) {
			Shield.SetActive(false);
			StartCoroutine (Shield_Recharge ());
		}
	}

	private IEnumerator Shield_Recharge()
	{
		while(true)
		{
			yield return new WaitForSeconds (rechargeTime);
			Shield.SetActive(true);
			hitCount = 0;
			break;
		}
	}

	// Update is called once per frame
	void Update () {
		
	}
}
