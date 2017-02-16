using UnityEngine;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class Hero_Scripts : MonoBehaviour {

    public int map_level;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public bool randomType()
    {
        if (Random.Range(1, 2) == 2) return true;
        else return false;
        
    }


    public int randomAttack(int tier , bool Attacker) {
        
        int tempAtt = 0;
        if (Attacker) tempAtt = Random.Range( (tier * 2), ((tier * tier) + (tier * 2)) );
        else tempAtt = Random.Range( (tier + 1), (tier * 3) );
        return tempAtt;
    }

    public int randomDef(int tier, bool defender) {

        int tempDef = 0;
        if (!defender) tempDef = Random.Range((tier * 2), ((tier * tier) + (tier * 2)));
        else tempDef = Random.Range((tier + 1), (tier * 3));
        return tempDef;
    }

    public int caculaHeath(int tier) {

        int temp = 0;

        temp = Random.Range((tier * 2), ((tier + 1) * 2));

        return temp;
    }

    public int randomTier()
    {
        if (Random.Range(0, 100) > 77) return map_level;
        else return map_level + 1;
    }
}
