using UnityEngine;
using System.Collections;

public class Player_Script : MonoBehaviour {

    public float moveSpeed;

    private Vector2 velocity;

    private int health;
    private int attack;
    private int defence;

    private int numTurn;
    private bool inBattle;



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        float dt = Time.deltaTime;

        UpdateInput();
        UpdateMovement(dt);
	}

    void UpdateInput()
    {
        velocity.Set(
            Input.GetAxis("Horizontal") * moveSpeed,
            Input.GetAxis("Vertical") * moveSpeed);
    }

    void UpdateMovement(float dt)
    {
        transform.Translate(velocity.x * dt, velocity.y * dt, 0);
    }

    public int GetAttack() { return attack; }
    public int GetDefence() { return defence; }
    public int GetHealth() { return health; }
    public Vector2 GetVelocity() { return velocity; }

    public void GotHit(int damg)
    {
        health -= damg;
    }

    public void IsDead()
    {
        // enabled = false;
    }

}
