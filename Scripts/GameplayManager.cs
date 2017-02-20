using UnityEngine;
using System.Collections;

public class GameplayManager : MonoBehaviour
{
    public Player_Script linkedPlayer;
    public MapGeneration linkedMap;

    public float visibilityRange = 10f;

    void UpdateFogState()
    {

    }

    // Use this for initialization
	void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {
        var c = linkedMap.GetComponentsInChildren<TileScript>();

        foreach (var i in c)
        {
            bool isInRange =
                (i.transform.position - linkedPlayer.transform.position).sqrMagnitude <= visibilityRange;
            var r = i.GetComponents<SpriteRenderer>();

            switch (i.fogState)
            {
                case FogState.Undiscovered:
                case FogState.Hidden:
                    if (isInRange)
                        i.fogState = FogState.Visible;
                    break;
                case FogState.Visible:
                    if (!isInRange)
                        i.fogState = FogState.Hidden;
                    break;
            }

            foreach (var i2 in r)
            {
                switch (i.fogState)
                {
                    case FogState.Undiscovered:
                        i2.color = new Color(0f, 0f, 0f);
                        break;
                    case FogState.Hidden:
                        i2.color = new Color(0.5f, 0.5f, 0.5f);
                        break;
                    case FogState.Visible:
                        i2.color = new Color(1f, 1f, 1f);
                        break;
                }
            }
        }
	}
}
