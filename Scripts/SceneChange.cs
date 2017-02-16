using UnityEngine;
using System.Collections;

using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    // Load game scene

    // Load saved game

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadMainMenuOptions()
    {
        SceneManager.LoadScene("MainMenu_Options");
    }

    // Load pause menu

    // Load inventory screen(s)

}
