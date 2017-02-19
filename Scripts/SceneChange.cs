using UnityEngine;
using System.Collections;

using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour {

    //private bool isPausePanelVisible = false;

    //public GameObject PausePanel = null;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    // Load game scene
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

    public void TogglePausePanel()
    {
        //if (Input.GetButtonDown("Pause Button"))
        //{
        //    isPausePanelVisible = true;
        //}
    }

    private void SetPausePanelVisibility()
    {
        //if (PausePanel != null)
        //{
        //    PausePanel.SetActive(isPausePanelVisible);
        //}
    }

    // Load inventory screen(s)
    public void LoadInventoryMain()
    {
        SceneManager.LoadScene("Inventory_Main");
    }

}
