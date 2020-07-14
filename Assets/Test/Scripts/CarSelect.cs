// by @unmanuel

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/*
    CarSelect: Main script of CarSelect.unity.
*/
public class CarSelect : MonoBehaviour {

    public CarStats[] stats;    // We will store car specs here
    public CarStats maxStats;   // Maximum specs to compare

    public GameObject[] cars;   // Example models

    public Text nameLabel;      // Name of the car

    // Spec bars
    public RectTransform speedBar, accelBar, brakingBar, handlingBar;
	
    // Car selection buttons
    public RectTransform[] carButtons;
    public RectTransform selector;
    
    // Current and previous selection
    int selectedIndex = 0, prevIndex = -1;

    void Start() {
    	RefreshStats();
    }

    public void ChangeCar(int index) {

    	prevIndex = selectedIndex;
    	selectedIndex = index;

    	RefreshStats();
    }

    public void GameStart() {

    	// car_index will store the last chosen car for future play sessions
        PlayerPrefs.SetInt("car_index", selectedIndex);

    	SceneManager.LoadScene("MainScene");
    }

    void RefreshStats() {

    	nameLabel.text = stats[selectedIndex].name;

    	speedBar.transform.localScale = new Vector3(stats[selectedIndex].maxSpeed / maxStats.maxSpeed, 1, 1);
    	accelBar.transform.localScale = new Vector3(stats[selectedIndex].acceleration / maxStats.acceleration, 1, 1);
    	brakingBar.transform.localScale = new Vector3(stats[selectedIndex].brakingTime / maxStats.brakingTime, 1, 1);
    	handlingBar.transform.localScale = new Vector3(stats[selectedIndex].handling / maxStats.handling, 1, 1);

        // Previous selection is disabled
    	if(prevIndex != -1) {

            cars[prevIndex].SetActive(false);
            
            cars[selectedIndex].transform.localRotation = cars[prevIndex].transform.localRotation;
        }

    	cars[selectedIndex].SetActive(true);

    	selector.localPosition = carButtons[selectedIndex].localPosition;
    }
}
