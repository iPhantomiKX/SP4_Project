using UnityEngine;
using System.Collections;

public class LivingEntity : MonoBehaviour, IDamageable
{
    protected enum Type {Player, Runner, Chaser, Sleeper, Wanderer};

    public struct TileCoord
    {
       public int x;
       public int y;

       public TileCoord(int _x, int _y)
        {
            x = _x;
            y = _y;
        }

       public void Set(int _x, int _y)
       {
           x = _x;
           y = _y;
       }

       public static bool operator ==(TileCoord c1, TileCoord c2)
       {
           return c1.x == c2.x && c1.y == c2.y;
       }

       public static bool operator !=(TileCoord c1, TileCoord c2)
       {
           return !(c1 == c2);
       }
    }

    protected TileCoord CurrentTileID;

    // ...
    protected A_Star_Script PathFinding;
    protected MapGeneration Map;
    public bool IMplayer;

    protected string myName;
    protected int tier;

    protected Type LivingType;
  
    protected int health;
    protected int attack;
    protected int defence;

    protected int MaxDefence;

    protected bool dead;
    protected bool Battle;
    protected int currentTurn;

    public event System.Action OnDeath;

    public void TakeHit(int damage)
    {
        if (defence >= damage)
        {
            defence -= damage;
        }
        else if (defence > 0)
        {
            int temp = damage;
            temp -= defence;
            defence = 0;
            health -= temp;
        }
        else
        {
            health -= damage;
        }
    }

    public virtual void Start()
    {
        PathFinding = FindObjectOfType(typeof(A_Star_Script)) as A_Star_Script;
        Map = FindObjectOfType(typeof(MapGeneration)) as MapGeneration;

        if (IMplayer)
        {
            health = 5;
            attack = 1;
            defence = 1;

            myName = "player";
            LivingType = Type.Player; 
        }
        else
        {
            myName = generateName();
            tier = randomTier();
            LivingType = randomType();

            attack = randomAttack(tier, LivingType);
            defence = randomDef(tier, LivingType);
            health = caculaHeath(tier);  
        }

        Vector3 temp = Map.PositionToCoord(transform.position);
        CurrentTileID.Set(Mathf.FloorToInt(temp.x), Mathf.FloorToInt(temp.y));

        dead = false;

        Battle = false;
        MaxDefence = defence;
        currentTurn = 0;
    }

    public void JoinBattle()
    {
        MaxDefence = defence;
        Battle = true;
    }

    public void LeaveBattle()
    {
        if(!dead)
        {
            defence = MaxDefence;
            Battle = false;
        }
    }

    protected void Die()
    {
        dead = true;
        if(OnDeath != null)
        {
            OnDeath();
        }
        GameObject.Destroy(gameObject);
    }


    // ai
    protected string generateName()
    {
        char[] List = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', ' ' };

        int ListLength = List.Length - 1;

        int nameLength = Random.Range(4, 8);

        string name = "";

        for (int i = 0; i < nameLength; i++)
        {
            int temp = Random.Range(0, ListLength);
            name += List[temp];
        }

        return name;
    }

    protected Type randomType()
    {

        int temp = Random.Range(1, 5);

        if (temp == 1) return Type.Runner;
        else if (temp == 2) return Type.Chaser;
        else if (temp == 3) return Type.Sleeper;
        else return Type.Wanderer;

    }

    protected int randomAttack(int tier, Type _type)
    {
        int tempAtt = 0;
        switch(_type)
        {
            case Type.Runner:
                {
                    tempAtt = Random.Range(1,5);
                    break;
                }
            case Type.Sleeper:
                {
                    tempAtt = Random.Range(1, 5);
                    break;
                }
            case Type.Chaser:
                {
                    tempAtt = Random.Range(1, 5);
                    break;
                }
            case Type.Wanderer:
                {
                    tempAtt = Random.Range(1, 5);
                    break;
                }
        }
        return tempAtt;
    }

    protected int randomDef(int tier, Type _type)
    {

        int tempDef = 0;
        switch (_type)
        {
            case Type.Runner:
                {
                    tempDef = Random.Range(1, 5);
                    break;
                }
            case Type.Sleeper:
                {
                    tempDef = Random.Range(1, 5);
                    break;
                }
            case Type.Chaser:
                {
                    tempDef = Random.Range(1, 5);
                    break;
                }
            case Type.Wanderer:
                {
                    tempDef = Random.Range(1, 2);
                    break;
                }
        }
        return tempDef;
    }

    protected int caculaHeath(int tier)
    {
        int temp = 0;

        temp = Random.Range(1,2);

        return temp;
    }

    protected int randomTier()
    {
        float sqrDstToCenter = transform.position.sqrMagnitude;
        float mapHeight = Map.maps[Map.mapIndex].mapSize.y * Map.tileSize;
        float mapWidth =  Map.maps[Map.mapIndex].mapSize.x * Map.tileSize;

        for (int percent = 60,i = 3 ; percent >= 0; percent -= 30, i-- )
        {
            float sqrDstForMapToCenter = (new Vector3(mapWidth * percent / 100, mapHeight * percent / 100, 0)).sqrMagnitude;

             if (sqrDstToCenter > sqrDstForMapToCenter)
             {
                 int temp;
                 temp = Random.Range(Map.mapIndex, 3 + Map.mapIndex);
                 return temp;
             }

        }

        return 0;
    }

    //

    protected bool calculateDistance(Vector3 other, float distance)
    {
        // to remove z position
        Vector3 temp = new Vector3(transform.position.x, transform.position.y, 0);
        
        float sqrDstToOther = (temp - other).sqrMagnitude;

        // Debug.Log(temp + " : " + other);
         Debug.Log(LivingType + " : " + sqrDstToOther + " ----" + distance);
        
        if(sqrDstToOther <= distance) return true;
        else return false;
    }

    public int GetHealth()
    {
        return health;
    }

    public int GetAttack()
    {
        return attack;
    }

    public int GetDefence()
    {
        return defence;
    }

    public int GetTurn()
    {
        return currentTurn;
    }

    public TileCoord GetCurrTileID()
    {
        return CurrentTileID;
    }
}