using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CardGenerator : MonoBehaviour {

    string[] NameOfCards = {"BronzeArmour",
                           "BronzeSword",
                           "SilverArmour",
                           "SilverSword",
                           "GoldArmour",
                           "GoldSword",
                           "GeneralsVisage",
                           "GeneralsBlade",
                           "ChallengersMantle",
                           "ChallengersDestructor"};

    public List<GameObject> Deck = new List<GameObject>();
    GameObject Card;
    int i;

	// Use this for initialization
	void Start () {
	
	}

    //WHEN PLAYER KILLS ENEMY
    void GenerateCard()
    {
        i = (Random.Range(0, NameOfCards.Length));
        Card = Instantiate(Resources.Load(NameOfCards[i]), new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        Card.GetComponent<CardData>();
        Card.transform.parent = this.transform;
    }

    void PickUpCard(GameObject DroppedCard)
    {
        Deck.Add(DroppedCard);
    }
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetKeyDown("space"))
        {
            GenerateCard();
        }
        if(Input.GetMouseButtonDown(0))
        {
            PickUpCard(Card);
        }
	}
}
