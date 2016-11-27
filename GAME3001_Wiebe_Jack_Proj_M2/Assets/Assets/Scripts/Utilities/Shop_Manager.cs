using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Shop_Manager : MonoBehaviour {

	public GameObject[] Bullets;
	public GameObject[] Lazers;
	public GameObject[] Missiles;

	public Text Money;

	public Toggle[] toggles;

	// Use this for initialization
	void Start () {
		//Bullets = GameObject.FindGameObjectsWithTag ("Bullet");
		//Lazers = GameObject.FindGameObjectsWithTag ("Lazer");
		//Missiles = GameObject.FindGameObjectsWithTag ("Missile");
		SetBulletUI ();
		SetLazerUI ();
		SetMissileUI ();
	}

	public void Purchas_Lazer(int value)
	{
		if (Player_Stats.instance.score < 10)
			return;
		else
			Player_Stats.instance.score -= 10 * value;

		Player_Stats.instance.lazer_level += value;
		if (Player_Stats.instance.lazer_level > 8) {
			Player_Stats.instance.lazer_level = 8;
			return;
		}
		if (Player_Stats.instance.lazer_level < 0) {
			Player_Stats.instance.lazer_level = 0;
			return;
		}
			
		SetLazerUI ();

	}

	public void SetLazerUI()
	{
		foreach (GameObject obj in Lazers) {

			obj.GetComponent<Image> ().color = new Color32 (255, 0, 0, 255);
		}
		for (int i = 0; i < Player_Stats.instance.lazer_level; i++) {
			//Debug.Log ("WTF");
			Lazers[i].GetComponent<Image>().color =  new Color32 (255, 255, 255, 255);
		}
	}

	public void Purchas_Bullet(int value)
	{
		if (Player_Stats.instance.score < 10)
			return;
		else
			Player_Stats.instance.score -= 10 * value;

		Player_Stats.instance.bullet_level += value;
		if (Player_Stats.instance.bullet_level > 8) {
			Player_Stats.instance.bullet_level = 8;
			return;
		}
		if (Player_Stats.instance.bullet_level < 0) {
			Player_Stats.instance.bullet_level = 0;
			return;
		}

		SetBulletUI ();

	}

	public void SetBulletUI()
	{
		foreach (GameObject obj in Bullets) {

			obj.GetComponent<Image> ().color = new Color32 (255, 0, 0, 255);
		}
		for (int i = 0; i < Player_Stats.instance.bullet_level; i++) {
			//Debug.Log ("WTF");
			Bullets[i].GetComponent<Image>().color =  new Color32 (255, 255, 255, 255);
		}
	}

	public void Purchas_Missile(int value)
	{
		if (Player_Stats.instance.score < 10)
			return;
		else
			Player_Stats.instance.score -= 10 * value;
		
		Player_Stats.instance.missile_level += value;
		if (Player_Stats.instance.missile_level > 8) {
			Player_Stats.instance.missile_level = 8;
			return;
		}
		if (Player_Stats.instance.missile_level < 0) {
			Player_Stats.instance.missile_level = 0;
			return;
		}


		SetMissileUI ();


	}

	public void SetCurrentWeapon(int type)
	{
		Player_Stats.instance.Set_Type ((P_TYPE)type);
		/*for (int i = 0; i < 3; i++) {
			toggles [i].isOn = false;
		}
		toggles [type].isOn = true;*/
	}

	public void SetMissileUI()
	{
		foreach (GameObject obj in Missiles) {

			obj.GetComponent<Image> ().color = new Color32 (255, 0, 0, 255);
		}
		for (int i = 0; i < Player_Stats.instance.missile_level; i++) {
			//Debug.Log ("WTF");
			Missiles[i].GetComponent<Image>().color =  new Color32 (255, 255, 255, 255);
		}
	}

	public void LoadNextRound()
	{
		Player_Stats.instance.round++;
		SceneManager.LoadScene ("Scene01");
	}

	// Update is called once per frame
	void Update () {
		Money.text = string.Concat ("Money: ", Player_Stats.instance.score);
	}
}
