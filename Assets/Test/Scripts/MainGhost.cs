// by @unmanuel

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
	MainGhost: Main script of MainScene.unity.
*/
public class MainGhost : MonoBehaviour {

	public Player Player;	// Player racer
	public AI AI;			// AI racer

	void Start() {
		AI.gameObject.SetActive(false);
	}

	/*
		LapComplete: Callback for the player racer each time a lap is complete.
	*/
	public void LapComplete() {
		if(Player.Character.LapCount == 1)
			AI.gameObject.SetActive(true);

		if(Player.Character.LapCount == 3)
			SceneManager.LoadScene("CarSelect");
	}

	/*
		EnhanceAI: Callback for the AI racer each time a lap is complete.
	*/
	public void EnhanceAI() {
		if(AI.Character.LapCount == 1)
			AI.ToggleEnhancedMode();
	}
}
