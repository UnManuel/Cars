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

    public CarSpecification[] Specifications;   // We will store car specs here
    public CarSpecification MaxSpecification;   // Maximum specs to compare

    public GameObject[] Cars;   // Example models

    public Text NameLabel;      // Name of the car

    // Spec bars
    public RectTransform SpeedBar, AccelerationBar, BrakingBar, HandlingBar;
	
    // Car selection buttons
    public RectTransform[] CarButtons;
    public RectTransform Selector;
    
    // Current and previous selection
    int SelectedIndex = 0, PreviousIndex = -1;

    /*
        Start: View is started with the default car at index zero
    */
    void Start() {
    	RefreshSpecification();
    }

    /*
        ChangeCar: Callback to replace the currently selected car.

        Params:

        index(int: 0 - Stars.Length - 1): Index of the car to select.
    */
    public void ChangeCar(int index) {

    	PreviousIndex = SelectedIndex;
    	SelectedIndex = index;

    	RefreshSpecification();
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
        RefreshSpecification: Car select view is fully refreshed based upon current car index.
    */
    void RefreshSpecification() {

    	NameLabel.text = Specifications[SelectedIndex].Name;

    	SpeedBar.transform.localScale = new Vector3(Specifications[SelectedIndex].MaxSpeed / MaxSpecification.MaxSpeed, 1, 1);
    	AccelerationBar.transform.localScale = new Vector3(Specifications[SelectedIndex].Acceleration / MaxSpecification.Acceleration, 1, 1);
    	BrakingBar.transform.localScale = new Vector3(Specifications[SelectedIndex].BrakingTime / MaxSpecification.BrakingTime, 1, 1);
    	HandlingBar.transform.localScale = new Vector3(Specifications[SelectedIndex].Handling / MaxSpecification.Handling, 1, 1);

        // Previous selection is disabled
    	if(PreviousIndex != -1) {

            Cars[PreviousIndex].SetActive(false);
            
            Cars[SelectedIndex].transform.localRotation = Cars[PreviousIndex].transform.localRotation;
        }

    	Cars[SelectedIndex].SetActive(true);

    	Selector.localPosition = CarButtons[SelectedIndex].localPosition;
    }
}
