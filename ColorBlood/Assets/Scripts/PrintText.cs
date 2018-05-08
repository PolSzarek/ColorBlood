using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PrintText : MonoBehaviour {

	public GameObject uiText;
	public GameObject uiPanel;
	public string txtToPrint;
	public int timeToWait;
	private Text myText;
	//private static int count; 

	void Start() {
		//count = 0;
		myText = uiText.gameObject.GetComponent<Text>();
		Debug.Log (myText.text);
	}


	void OnTriggerEnter(Collider player) {
		if (player.gameObject.tag == "Player" /*&& count == 0*/) {
			myText.text = txtToPrint;
			Debug.Log (myText.text);
			uiPanel.SetActive (true);
			uiText.SetActive (true);
			StartCoroutine ("WaitForSec");
		}
	}

	IEnumerator WaitForSec() {
		yield return new WaitForSeconds (timeToWait);
		uiText.SetActive (false);
		uiPanel.SetActive (false);
		//count = 1;
	}

}
