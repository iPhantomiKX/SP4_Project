using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;


public class AI_Scripts : MonoBehaviour{

    public Text Name_Text;

    public Hero_Scripts other;

    private string name;
    private int tier;
    private int attack;
    private int defence;
    private int health;
    private bool Attacker;

    public GameObject information_board;

    public GameObject Stats_Bar;
    public Text Att_Text;
    public Text HP_Text;
    public Text Def_Text;


	// Use this for initialization
	void Start () {

        name = "Baby_Haier";
        tier = other.randomTier();
        Attacker = other.randomType();
        attack = other.randomAttack(tier, Attacker);
        defence = other.randomDef(tier, Attacker);
        health = other.caculaHeath(tier);

        Name_Text.text = name;
        Att_Text.text = attack.ToString();
        HP_Text.text = health.ToString();
        Def_Text.text = defence.ToString();

        information_board.SetActive(false);
        //information_board.transform,RectTr = new Vector3(0, 0, 0);

	}
	
	// Update is called once per frame
	void Update () {

        Att_Text.text = attack.ToString();
        HP_Text.text = health.ToString();
        Def_Text.text = defence.ToString();

	}

    public void SetHoverOver(bool active)
    {
        Stats_Bar.SetActive(!active);
        information_board.SetActive(active);
    }

    public string GetName() { return name; }
    public int GetAttack() { return attack; }
    public int GetDefence() { return defence; }
    public int GetHealth() { return health; }

    public void GotHit(int damg)
    {
        if(damg > defence)
        {
            damg -= defence;
            defence = 0 ;
        }
        else
        {
            defence -= damg;
            damg = 0;
        }

        health -= damg;
    }

    public void IsDead()
    {
       // enabled = false;
    }


}
