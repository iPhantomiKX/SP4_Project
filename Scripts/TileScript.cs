using UnityEngine;
using System.Collections;

public enum FogState
{
    Undiscovered = 0,
    Hidden,
    Visible,
}

public class TileScript : MonoBehaviour {

    public FogState fogState;

	// Use this for initialization
	void Start () {
        fogState = FogState.Undiscovered;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
