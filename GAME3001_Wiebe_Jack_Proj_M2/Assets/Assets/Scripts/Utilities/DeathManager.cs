using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class DeathManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Player_Stats.instance.bullet_level = 1;
		Player_Stats.instance.missile_level = 0;
		Player_Stats.instance.lazer_level = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Reload_Game()
	{
		Destroy (Player_Stats.instance.gameObject);
		//Player_Stats.instance.isGameMode = true;
		SceneManager.LoadScene ("Scene01");
	}

	public void Exit_Game()
	{
		Destroy (Player_Stats.instance.gameObject);
		//Destroy (Player_Stats.instance);
		SceneManager.LoadScene ("Menu");
	}

}
