using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Spawner : MonoBehaviour {

	[SerializeField]
	public const float basic_enemy_multiplier = 3.0f;
	[SerializeField]
	public const float boss_enemy_multiplier = 1.0f;
	[SerializeField]
	public const float evasive_enemy_multiplier = 2.0f;

	public int basic_enemy_count;
	public int boss_enemy_count;
	public int evasive_enemy_count;

	public int spawn_delay;
	public int startingCount;
	public Text count;

	public GameObject Basic_Enemy;
	public GameObject Boss_Enemy;
	public GameObject Evasive_Enemy;

	public GameObject[] spawnPoints;

	public List<GameObject> spawnPool;

	[SerializeField]
	private int _curRround;

	public void StartRound()
	{
		//Debug.Break ();
		spawnPoints = GameObject.FindGameObjectsWithTag ("SpawnPoint");
		GameObject temp;
		_curRround = Player_Stats.instance.round;
		if (_curRround % 2 == 0)
			basic_enemy_count = _curRround;
		if (_curRround % 3 == 0)
			evasive_enemy_count = _curRround;
		if (_curRround % 4 == 0)
			boss_enemy_count = _curRround;

		//Debug.Log (basic_enemy_count);

		for (int i = 0; i <= _curRround; i++) {
			for (int j = 0; j < (int)(basic_enemy_count * basic_enemy_multiplier); j++) {
				temp = Instantiate (Basic_Enemy, spawnPoints [Random.Range (0, spawnPoints.Length)].transform.position, Quaternion.identity) as GameObject;
				temp.SetActive (false);
				temp.GetComponent<Basic_Enemy_AI> ().spawner = this;
				spawnPool.Add (temp);
			}
			for (int j = 0; j < (int)(boss_enemy_count * boss_enemy_multiplier); j++) {
				temp = Instantiate (Boss_Enemy, spawnPoints [Random.Range (0, spawnPoints.Length)].transform.position, Quaternion.identity) as GameObject;
				temp.SetActive (false);
				temp.GetComponent<Basic_Enemy_AI> ().spawner = this;
				spawnPool.Add (temp);
			}
			for (int j = 0; j < (int)(evasive_enemy_count * evasive_enemy_multiplier); j++) {
				temp = Instantiate (Evasive_Enemy, spawnPoints [Random.Range (0, spawnPoints.Length)].transform.position, Quaternion.identity) as GameObject;
				temp.SetActive (false);
				temp.GetComponent<Basic_Enemy_AI> ().spawner = this;
				spawnPool.Add (temp);
			}
		}

		startingCount = spawnPool.Count;
		StartCoroutine (Spawning ());

	}

	private IEnumerator Spawning()
	{
		while (true) {
			
			yield return new WaitForSeconds (spawn_delay);

			for (int i = 0; i < spawnPool.Count; i++) {
				if (spawnPool [i] != null) {
					if (!spawnPool [i].activeInHierarchy) {
						spawnPool [i].SetActive (true);
						break;
					}
				}
			}
				
		}
	}

	void Awake()
	{
		
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log (spawnPool.Count);
		count.text = string.Concat(spawnPool.Count, "/", startingCount);
		if (spawnPool.Count <= 0)
			LoadNextRound ();
	}

	public void LoadNextRound()
	{
		//Player_Stats.instance.isShopMode = true;
		SceneManager.LoadScene ("Shop");
	}
}
