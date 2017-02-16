using UnityEngine;
using System.Collections;

public class Player_Script : MonoBehaviour {

    public float speed;

    private int health;
    private int attack;
    private int defence;


    private int numTurn;

    private bool inBattle;



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Movement();
	}

    void Movement()
    {
        // dont care about the naming first


        float forwardMovement = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        float turnMovement = Input.GetAxis("Horizontal") * speed * Time.deltaTime;

        transform.Translate(Vector3.up  * forwardMovement);
        transform.Translate(Vector3.right * turnMovement);
    }


    public int GetAttack() { return attack; }
    public int GetDefence() { return defence; }
    public int GetHealth() { return health; }

    public void GotHit(int damg)
    {
        health -= damg;
    }

    public void IsDead()
    {
        // enabled = false;
    }

}
