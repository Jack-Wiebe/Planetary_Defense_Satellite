using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class DeathManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Reload_Game()
	{
		Player_Stats.instance.isGameMode = true;
		SceneManager.LoadScene ("Scene01");
	}

	public void Exit_Game()
	{
		//Destroy (Player_Stats.instance);
		SceneManager.LoadScene ("Menu");
	}

}
