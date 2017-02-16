using UnityEngine;
using System.Collections;

using UnityEngine.SceneManagement;

public class GameInventory : MonoBehaviour {

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

    // Go to inventory
    public void LoadInventoryMain()
    {
        SceneManager.LoadScene("Inventory_Main");
    }

    // Inventory Scenes
}
