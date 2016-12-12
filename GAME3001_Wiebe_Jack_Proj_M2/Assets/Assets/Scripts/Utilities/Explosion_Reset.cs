using UnityEngine;
using System.Collections;

public class Explosion_Reset : MonoBehaviour {

	private SpriteRenderer m_sprite;

	// Use this for initialization
	void Start () {
		m_sprite = GetComponent<SpriteRenderer> ();
	}

	// Update is called once per frame
	void Update () {
		if (!m_sprite.enabled) {
			m_sprite.enabled = true;
			this.gameObject.SetActive (false);
		}
	}
}
