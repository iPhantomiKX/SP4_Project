using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;


public class AI_Scripts : LivingEntity{

    public enum State { IDLE, WANDER, CHASE, SLEEP, ESCAPE , NONE};

    public GameObject Stats_Bar;
    public Text Name_Text;
    public Text Att_Text;
    public Text HP_Text;
    public Text Def_Text;

    public GameObject information_board;
    public Text Name_Text2;
    public Text Att_Text2;
    public Text HP_Text2;
    public Text Def_Text2;
   
   // Transform target;
    Player_Script player;

    Vector3 playerPosition;
    Vector3 DesNormal;
    TileCoord DesTileID;

    float speed;

    State TurnState;
    bool RunOnce;

    // for Sleeper to know that he got hit and awaken
    int MaxHp;

    // Use this for initialization
    public override void Start()
    {
        base.Start();

        player = FindObjectOfType(typeof(Player_Script)) as Player_Script;

        //Stats bar
        Name_Text.text = myName;
        Att_Text.text = attack.ToString();
        HP_Text.text = health.ToString();
        Def_Text.text = defence.ToString();
        Stats_Bar.SetActive(true);

        //information board
        Name_Text2.text = myName;
        Att_Text2.text = attack.ToString();
        HP_Text2.text = health.ToString();
        Def_Text2.text = defence.ToString();
        information_board.SetActive(false);

        TurnState = State.NONE;
        RunOnce = false;
        //for sleeper
        MaxHp = health;

        speed = player.speed;

        DesTileID = CurrentTileID;
    }


    void Update()
    {
        playerPosition = new Vector3(player.transform.position.x,player.transform.position.y,0f);

        Vector3 temp = Map.PositionToCoord(transform.position);
        CurrentTileID.Set(Mathf.FloorToInt(temp.x), Mathf.FloorToInt(temp.y));

        // StartCoroutine(At


        if (CurrentTileID == player.GetCurrTileID() && !Battle)
        {
            gameObject.tag = "InBattle";
            JoinBattle();
            player.JoinBattle();
        }

        if (currentTurn < player.GetTurn() && !Battle)
        {
            if (!RunOnce)
            {
                CheckState();
                UpdateState();
            }

            Movement();
        }

        if (health <= 0 && !dead) Die();
    }

    void CheckState()
    {
        switch(LivingType)
        {
            case Type.Runner:
                {
                    if (calculateDistance(playerPosition, 25f * Map.tileSize))
                    {
                        TurnState = State.ESCAPE;
                    }
                    else TurnState = State.IDLE;

                    break;
                }
            case Type.Chaser:
                {
                    if (calculateDistance(playerPosition, 40f * Map.tileSize))
                    {
                        TurnState = State.CHASE;
                    }
                    else TurnState = State.WANDER;

                    break;
                }
            case Type.Wanderer:
                {
                    if(possibility(30))
                    {
                        TurnState = State.IDLE;
                    }
                    else TurnState = State.WANDER;

                    break;
                }
            case Type.Sleeper:
                {
                    if (health == MaxHp)
                    {
                        TurnState = State.SLEEP;
                    }
                    else TurnState = State.CHASE;

                    break;
                }
        }
    }

    void UpdateState()
    {
        switch(TurnState)
        {
            case State.CHASE:
                {
                    Vector3 Destination = PathFinding.FindPath(new Vector3(CurrentTileID.x, CurrentTileID.y, 0), new Vector3(player.GetCurrTileID().x, player.GetCurrTileID().y, 0));
                    DesTileID = new TileCoord((int)Destination.x, (int)Destination.y);
                    //transform.position = Map.CoordToPosition((int)DesTileID.x, (int)DesTileID.y);
                    break;
                }
            case State.WANDER:
                {
                    //while (true) // need to be (CheckTile(DesTile.Set) == 0)
                    //{
                    //    int temp = Random.Range(0, 5);

                    //    if (temp == 0) // right
                    //    {
                    //        DesTileID.Set(CurrentTileID.x + 1, CurrentTileID.y);
                    //    }
                    //    else if (temp == 1) // up
                    //    {
                    //        DesTileID.Set(CurrentTileID.x, CurrentTileID.y + 1);
                    //    }
                    //    else if (temp == 2) // left
                    //    {
                    //        DesTileID.Set(CurrentTileID.x - 1, CurrentTileID.y);
                    //    }
                    //    else // 4 down
                    //    {
                    //        DesTileID.Set(CurrentTileID.x, CurrentTileID.y - 1);
                    //    }
                    //}

                    Vector3 temp = Map.PositionToCoord(transform.position);
                    DesTileID = new TileCoord((int)temp.x,(int)temp.y);
                    break;
                }
            case State.ESCAPE:
                {
                    // dont know how yet
                    break;
                }
            case State.IDLE:
                {
                   // nothing 
                    TurnState = State.NONE;
                    break;
                }
            case State.SLEEP:
                {
                    // nothing 
                    TurnState = State.NONE;
                    break;
                }
        }
        RunOnce = true;
    }

    void Movement()
    {
       if(TurnState == State.CHASE || TurnState == State.WANDER || TurnState == State.ESCAPE)
       {
           Vector3 Temp = Map.CoordToPosition(DesTileID.x, DesTileID.y);
           Debug.Log(DesTileID.x + " : " + DesTileID.y);
           StartCoroutine(MoveTo(Temp, speed));
       }
        currentTurn++;
        RunOnce = false;
    }

    bool possibility(int Odds)
    {
        return (Random.Range(0,101) < Odds);
    }

    void UpdateStatsText()
    {
        //Stats bar
        Att_Text.text = attack.ToString();
        HP_Text.text = health.ToString();
        Def_Text.text = defence.ToString();

        //information board
        Att_Text2.text = attack.ToString();
        HP_Text2.text = health.ToString();
        Def_Text2.text = defence.ToString();
    }

    public void SetHoverOver(bool active)
    {
        Stats_Bar.SetActive(!active);
        information_board.SetActive(active);
    }
}
