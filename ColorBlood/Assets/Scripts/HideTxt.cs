using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HideTxt : MonoBehaviour {

	public GameObject uiText;
	public GameObject uiPanel;

	void OnTriggerEnter(Collider player) {
		if (player.gameObject.tag == "Player") {
			uiPanel.SetActive (false);
			uiText.SetActive (false);
		}
	}
}
