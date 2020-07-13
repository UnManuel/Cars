// by @unmanuel

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainGhost : MonoBehaviour {

	public Player player;
	public AI AI;

	void Start() {
		AI.gameObject.SetActive(false);
	}

	public void EnableAI() {
		if(player.character.lapCount == 1)
			AI.gameObject.SetActive(true);

		if(player.character.lapCount == 3)
			SceneManager.LoadScene("CarSelect");
	}

	public void EnhanceAI() {
		if(AI.character.lapCount == 1)
			AI.Enhance();
	}
}
