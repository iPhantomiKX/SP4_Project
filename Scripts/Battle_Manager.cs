using UnityEngine;
using System.Collections;

public class Battle_Manager : MonoBehaviour {

    public Player_Script Player;

    GameObject FindThis;
    AI_Scripts Enemy;

    bool Battle;
    bool BattleStart;

    bool PlayerTurn;

    float waitTime;

    void Start()
    {
        Battle = false;
        BattleStart = false;
        waitTime = 3f;
        
    }

    void BattleStarts()
    {
        FindThis = GameObject.FindGameObjectWithTag("InBattle");
        Enemy = FindThis.GetComponent(typeof(AI_Scripts)) as AI_Scripts;
        Enemy.OnDeath += onEnemyDeath;

        BattleStart = true;

        PlayerTurn = true;
        Player.SetMyTurn(true);
    }

	void Update () {

        if (Player.GetBattle() && !Battle) Battle = true;

        if (Battle)
        {
            if (!BattleStart) BattleStarts();
            else
            {
                if (!Player.GetMyTurn() && PlayerTurn)
                {
                    PlayerTurn = false;
                    Enemy.SetMyTurn(true);
                    waitTime = 3f;
                    Enemy.TakeHit(Player.GetAttack());
                }
                if (!Enemy.GetMyTurn() && !PlayerTurn)
                {
                    PlayerTurn = true;
                    Player.SetMyTurn(true);
                    waitTime = 3f;
                    Player.TakeHit(Enemy.GetAttack());
                }

                waitTime -= Time.deltaTime;

                if (waitTime < 0)
                {
                    if (PlayerTurn)
                    {
                        StartCoroutine(Player.Attack(Enemy.transform.position));
                    }
                    else
                    {
                        StartCoroutine(Enemy.Attack(Player.transform.position));
                    }
                }
            }

            if (Player.GetDeath())
            {
                
                Battle = false;
                BattleStart = false;
                Player.LeaveBattle();
                Debug.Log("Player L0se");
            }
            if (Enemy.GetDeath())
            {
                Enemy.OnDeath += onEnemyDeath;  
            }
        }
      
	}

    void EndTurn(bool _playerTurn)
    {
        Player.SetMyTurn(_playerTurn);
        Enemy.SetMyTurn(!_playerTurn);
    }

     public IEnumerator Wait(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            EndTurn(false);
        }

    }

    void onEnemyDeath()
     {
         Debug.Log("Drop Card");
         Battle = false;
         BattleStart = false;
         Player.LeaveBattle();
     }

}
