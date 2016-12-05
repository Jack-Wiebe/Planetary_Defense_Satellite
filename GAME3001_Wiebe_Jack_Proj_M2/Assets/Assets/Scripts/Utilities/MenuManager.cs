using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

	public GameObject Instrutions;
	public GameObject Menu;


	public void ToggleInstructions()
	{
		Instrutions.SetActive (!Instrutions.activeInHierarchy);
		Menu.SetActive (!Instrutions.activeInHierarchy);
	}

	public void Start_Game()
	{
		if (Player_Stats.instance != null)
			Player_Stats.instance.isGameMode = true;
		SceneManager.LoadScene ("Scene01");
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
