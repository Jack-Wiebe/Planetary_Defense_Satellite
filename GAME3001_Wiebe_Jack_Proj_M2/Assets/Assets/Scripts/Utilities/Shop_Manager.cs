using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Shop_Manager : MonoBehaviour {

	public GameObject[] Bullets;
	public GameObject[] Lazers;
	public GameObject[] Missiles;

	public Text Money;
	public Text Weapon;

	public Sprite bulletSprite;
	public Sprite missileSpite;
	public Sprite lazerSprite;
	public Image currentSprite;

	public Toggle bulletTog;
	public Toggle lazerTog;
	public Toggle missileTog;

	// Use this for initialization
	void Start () {
		switch (Player_Stats.instance.Get_Type ()) {
		case(P_TYPE.BULLET):
			bulletTog.isOn = true;
			break;
		case(P_TYPE.LAZER):
			lazerTog.isOn = true;
			break;
		case(P_TYPE.MISSILE):
			missileTog.isOn = true;
			break;
		}
		//Bullets = GameObject.FindGameObjectsWithTag ("Bullet");
		//Lazers = GameObject.FindGameObjectsWithTag ("Lazer");
		//Missiles = GameObject.FindGameObjectsWithTag ("Missile");
		SetBulletUI ();
		SetLazerUI ();
		SetMissileUI ();
	}

	public void Purchas_Lazer(int value)
	{
		Player_Stats.instance.score -= 200 * value;
		if(Player_Stats.instance.score < 0){
			Player_Stats.instance.score += 200;
			return;
		}
		Player_Stats.instance.lazer_level += value;
		if (Player_Stats.instance.lazer_level > 8) {
			if(value > 0)
				Player_Stats.instance.score += 200;
			Player_Stats.instance.lazer_level = 8;
			return;
		}
		if (Player_Stats.instance.lazer_level < 0) {
			//Player_Stats.instance.score -= 200 * value;
			if(value < 0)
				Player_Stats.instance.score -= 200;
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
		Player_Stats.instance.l_speed += 1;
		Player_Stats.instance.l_dmg = 10 * Player_Stats.instance.lazer_level;

	}

	public void Purchas_Bullet(int value)
	{
		//if (Player_Stats.instance.score < 0) {
		//	return;
		//}
		/*Player_Stats.instance.score -= 200 * value;
		if (Player_Stats.instance.score < 0) {
			
			return;
		}*/

		Player_Stats.instance.score -= 200 * value;
		if(Player_Stats.instance.score < 0){
			Player_Stats.instance.score += 200;
			return;
		}
		Player_Stats.instance.bullet_level += value;
		if (Player_Stats.instance.bullet_level > 8) {
			if(value > 0)
				Player_Stats.instance.score += 200;
			Player_Stats.instance.bullet_level = 8;
			return;
		}
		if (Player_Stats.instance.bullet_level < 0) {
			//Player_Stats.instance.score -= 200 * value;
			if(value < 0)
				Player_Stats.instance.score -= 200;
			Player_Stats.instance.bullet_level = 0;
			return;
		}

			
		//else if(Player_Stats.instance.score > 0)
		//	Player_Stats.instance.score -= 200 * value;

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

		Player_Stats.instance.b_speed += 1;
		Player_Stats.instance.b_dmg = 10 * Player_Stats.instance.bullet_level;
	}

	public void Purchas_Missile(int value)
	{
		//if (Player_Stats.instance.score < 200)
		//	return;

		Player_Stats.instance.score -= 200 * value;
		if(Player_Stats.instance.score < 0){
			Player_Stats.instance.score += 200;
			return;
		}
		Player_Stats.instance.missile_level += value;
		if (Player_Stats.instance.missile_level > 8) {
			if(value > 0)
				Player_Stats.instance.score += 200;
			Player_Stats.instance.missile_level = 8;
			return;
		}
		if (Player_Stats.instance.missile_level < 0) {
			//Player_Stats.instance.score -= 200 * value;
			if(value < 0)
				Player_Stats.instance.score -= 200;
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

		Player_Stats.instance.m_speed += 1;
		Player_Stats.instance.m_dmg = 10 * Player_Stats.instance.missile_level;

	}

	public void LoadNextRound()
	{
		Player_Stats.instance.round++;
		Player_Stats.instance.Set_Health (Player_Stats.instance.MAX_HEALTH);
		Player_Stats.instance.isGameMode = true;
		//Player_Stats.instance.Set_Stats (Player_Stats.instance.Get_Type());
		SceneManager.LoadScene ("Scene01");
	}

	// Update is called once per frame
	void Update () {
		Money.text = string.Concat ("Money: ", Player_Stats.instance.score);
		switch (Player_Stats.instance.Get_Type ()) {
		case P_TYPE.BULLET:
			currentSprite.sprite = bulletSprite;
			Weapon.text = "Current Weapon: BULLET";
			break;
		case P_TYPE.MISSILE:
			currentSprite.sprite = missileSpite;
			Weapon.text = "Current Weapon: MISSILE";
			break;
		case P_TYPE.LAZER:
			currentSprite.sprite = lazerSprite;
			Weapon.text = "Current Weapon: LAZER";
			break;
		}
	}
}
