using UnityEngine;
using System.Collections;

public class GameplayManager : MonoBehaviour
{
    public int boardWidth, boardHeight;
    public float tileSize;

    public ArrayList rows;

    // Use this for initialization
	void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    public void InitializeBoard(int boardWidth, int boardHeight, float tileSize)
    {
        this.tileSize = Mathf.Max(tileSize, 10.0f);

        for (int h = 0; h < boardHeight; ++h)
        {
            
        }
    }
}
