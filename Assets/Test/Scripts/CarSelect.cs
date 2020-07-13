// by @unmanuel

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CarSelect : MonoBehaviour {

    public CarStats[] stats;
	public CarStats maxStats;

    public GameObject[] cars;

	public Text nameLabel;

    public RectTransform speedBar, accelBar, brakingBar, handlingBar;
	
    public RectTransform[] carButtons;
	public RectTransform selector;
    
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

    	PlayerPrefs.SetInt("car_index", selectedIndex);

    	SceneManager.LoadScene("MainScene");
    }

    void RefreshStats() {

    	nameLabel.text = stats[selectedIndex].name;

    	speedBar.transform.localScale = new Vector3(stats[selectedIndex].maxSpeed / maxStats.maxSpeed, 1, 1);
    	accelBar.transform.localScale = new Vector3(stats[selectedIndex].acceleration / maxStats.acceleration, 1, 1);
    	brakingBar.transform.localScale = new Vector3(stats[selectedIndex].brakingTime / maxStats.brakingTime, 1, 1);
    	handlingBar.transform.localScale = new Vector3(stats[selectedIndex].handling / maxStats.handling, 1, 1);

    	if(prevIndex != -1) {

    		cars[prevIndex].SetActive(false);
            
            cars[selectedIndex].transform.localRotation = cars[prevIndex].transform.localRotation;
        }

    	cars[selectedIndex].SetActive(true);

    	selector.localPosition = carButtons[selectedIndex].localPosition;
    }
}
