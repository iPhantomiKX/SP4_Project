using UnityEngine;
using System.Collections;

using UnityEngine.SceneManagement;

public class GamePause : MonoBehaviour {

    private bool isPausePanelVisible = false;

    public GameObject PausePanel = null;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    // Return to game
    public void LoadGame()
    {
        SceneManager.LoadScene("Game");
    }

    // Go to Pause Menu
    public void TogglePausePanel()
    {
        if (Input.GetButtonDown("Pause Button"))
        {
            isPausePanelVisible = true;
        }
    }

    private void SetPausePanelVisibility()
    {
        if (PausePanel != null)
        {
            PausePanel.SetActive(isPausePanelVisible);
        }
    }

    // Load Options

    // (Save &) Exit
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
