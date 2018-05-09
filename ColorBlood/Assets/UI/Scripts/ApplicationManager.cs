using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ApplicationManager : MonoBehaviour {

	public enum Scenes {
		MAIN = 0,
		LEVEL1 = 1,
		LEVEL2 = 2
	}

	private float FRAME_TIME = 0.1f;

	public GameObject ui;
	private bool gamePaused = false;
	private float lastFrame = 0;


	public void GetInput() {
		if (Time.time - lastFrame > FRAME_TIME) {
			if (Input.GetButton("Escape")) {
				PauseGame(!gamePaused);
			}
			lastFrame = Time.time;
		}
	}

	public void Quit () 
	{
		#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
		#else
		Application.Quit();
		#endif
	}

	public void ChargeScene(int scene) {
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
		if (gamePaused) {
			PauseGame(!gamePaused);
		}


		Scenes eScene = (Scenes)scene;
		switch(eScene) {
			case Scenes.MAIN:
				break;
			case Scenes.LEVEL1:
			case Scenes.LEVEL2:
				break;
		}
	}

	public void PauseGame(bool pause) {
		Time.timeScale = pause ? 0 : 1;
		ui.gameObject.SetActive(pause);
		Cursor.visible = pause;
		gamePaused = pause;
	}
}
