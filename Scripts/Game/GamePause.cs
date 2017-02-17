using UnityEngine;
using System.Collections;

using UnityEngine.SceneManagement;

public class GamePause : MonoBehaviour {

    public GameObject PausePanel, Options;

	// Use this for initialization
	void Start ()
    {
        PausePanel = GameObject.Find("PausePanel");
	}
	
    public void OpenPausePanel()
    {
        PausePanel.SetActive(true);
    }

    public void ClosePausePanel()
    {
        PausePanel.SetActive(false);
    }

	void OnMouseDown()
    {
        if (gameObject.name == "Pause")
        {
            PausePanel.SetActive(true);
        }

        else if (gameObject.name == "Return")
        {
            PausePanel.SetActive(false);
        }

        else if (gameObject.name == "Options")
        {
            //load main menu option page
            SceneManager.LoadScene("MainMenu_Options");
        }

        else if (gameObject.name == "Exit")
        {
            //save file, exit to main menu
            SceneManager.LoadScene("MainMenu");
            PausePanel.SetActive(false);
        }
    }

}

/* Load Options from Main Menu
 * 
 */
