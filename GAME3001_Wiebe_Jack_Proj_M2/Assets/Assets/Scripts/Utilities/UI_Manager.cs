using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour {
	public Text scoreBoard;
	public Text healthBoard;
	public Text roundBoard;


	// Use this for initialization
	void Start () {
		scoreBoard = GameObject.Find ("Score").GetComponent<Text>();
		healthBoard = GameObject.Find ("Health").GetComponent<Text>();
		roundBoard = GameObject.Find ("Round").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		
		scoreBoard.text = string.Concat ("Score: ", Player_Stats.instance.score);
		healthBoard.text = string.Concat ("Health: ", Player_Stats.instance.Get_Health());
		roundBoard.text = string.Concat ("Round: ", Player_Stats.instance.round);
	
	}
}
