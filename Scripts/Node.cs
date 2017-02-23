using UnityEngine;
using System.Collections;

public class Node  {

    public enum tile_type{
        FLOOR,
        WALL,


    }

    public tile_type Type;
    public Vector3 worldPosition;
    public Vector3 tilePosition;

    public int G_value;
    public int H_value;

    public Node parent;

    public Node(tile_type _type, Vector3 _worldPos, Vector3 _tilePos)
    {
        Type = _type;
        worldPosition = _worldPos;
        tilePosition = _tilePos;
    }

    public void SetType(tile_type _type)
    {
        Type = _type;
    }

    public int F_value
    {
        get { return G_value + H_value; }
    }


}
