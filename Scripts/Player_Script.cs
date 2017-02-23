using UnityEngine;
using System.Collections;

public class Player_Script : LivingEntity {

    public float speed;

    TileCoord PreTileID;

	// Use this for initialization
    public override void Start()
    {
        base.Start();
        PreTileID = CurrentTileID;
	}
	
	// Update is called once per frame
	void Update () {

        Vector3 temp = Map.PositionToCoord(transform.position);
        CurrentTileID.Set(Mathf.FloorToInt(temp.x), Mathf.FloorToInt(temp.y));

        //Debug.Log(CurrentTileID.x);
        //Debug.Log(CurrentTileID.y);

        if(!Battle) Movement();
        if(health <= 0 && !dead)  Die();
	}

    void Movement()
    {
        float forwardMovement = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        float SideMovement = Input.GetAxis("Horizontal") * speed * Time.deltaTime;

        transform.Translate(Vector3.up  * forwardMovement);
        transform.Translate(Vector3.right * SideMovement);

        if (PreTileID != CurrentTileID)
        {
            currentTurn++;
            PreTileID = CurrentTileID;
        }
    }
}
