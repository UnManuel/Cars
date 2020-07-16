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

    public CarStats[] Stats;    // We will store car specs here
    public CarStats MaxStats;   // Maximum specs to compare

    public GameObject[] Cars;   // Example models

    public Text NameLabel;      // Name of the car

    // Spec bars
    public RectTransform SpeedBar, AccelBar, BrakingBar, HandlingBar;
	
    // Car selection buttons
    public RectTransform[] CarButtons;
    public RectTransform Selector;
    
    // Current and previous selection
    int SelectedIndex = 0, PrevIndex = -1;

    void Start() {
    	RefreshStats();
    }

    /*
        ChangeCar: Callback to replace the currently selected car.

        Params:

        index(int: 0 - Stars.Length - 1): Index of the car to select.
    */
    public void ChangeCar(int index) {

    	PrevIndex = SelectedIndex;
    	SelectedIndex = index;

    	RefreshStats();
    }

    /*
        GameStart: Current car is stored then gameplay screen is loaded.
    */
    public void GameStart() {

    	// car_index will store the last chosen car for future play sessions
        PlayerPrefs.SetInt("car_index", SelectedIndex);

        // Gameplay scene is loaded
    	SceneManager.LoadScene("MainScene");
    }

    /*
        RefreshStats: Car select view is fully refreshed based upon current car index.
    */
    void RefreshStats() {

    	NameLabel.text = Stats[SelectedIndex].Name;

    	SpeedBar.transform.localScale = new Vector3(Stats[SelectedIndex].MaxSpeed / MaxStats.MaxSpeed, 1, 1);
    	AccelBar.transform.localScale = new Vector3(Stats[SelectedIndex].Acceleration / MaxStats.Acceleration, 1, 1);
    	BrakingBar.transform.localScale = new Vector3(Stats[SelectedIndex].BrakingTime / MaxStats.BrakingTime, 1, 1);
    	HandlingBar.transform.localScale = new Vector3(Stats[SelectedIndex].Handling / MaxStats.Handling, 1, 1);

        // Previous selection is disabled
    	if(PrevIndex != -1) {

            Cars[PrevIndex].SetActive(false);
            
            Cars[SelectedIndex].transform.localRotation = Cars[PrevIndex].transform.localRotation;
        }

    	Cars[SelectedIndex].SetActive(true);

    	Selector.localPosition = CarButtons[SelectedIndex].localPosition;
    }
}
