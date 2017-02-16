using UnityEngine;
using System.Collections;

using UnityEngine.SceneManagement;

public class MenuScenes : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void LoadGame()
    {
        SceneManager.LoadScene("Game");
    }

    // Load saved game

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadMainMenuOptions()
    {
        SceneManager.LoadScene("MainMenu_Options");
    }

    // Exit Application

}
