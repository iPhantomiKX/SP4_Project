using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class A_Star_Script : MonoBehaviour {

    public bool FourDir; // 4 dir a* nor 8 dir a*  check

    MapGeneration grid;

    void Awake() {
        grid = GetComponent<MapGeneration>();
    }

    // string, vector3
    public Vector3 FindPath(Vector3 startPos, Vector3 targetPos)
    {
       // Node startNode = grid.NodeFromWorldPoint(startPos);
       // Node targetNode = grid.NodeFromWorldPoint(targetPos);

        Node startNode = grid.NodeFromTilePoint(startPos);
        Node targetNode = grid.NodeFromTilePoint(targetPos);


        List<Node> openSet = new List<Node>();
        HashSet<Node> closedSet = new HashSet<Node>();
        
        openSet.Add(startNode);

        while (openSet.Count > 0)
        {
            Node currentNode = openSet[0];
            for(int i = 1; i<openSet.Count; i++)
            {
                if( openSet[i].F_value < currentNode.F_value || ( openSet[i].F_value == currentNode.F_value && openSet[i].H_value < currentNode.H_value) )
                {
                    currentNode = openSet[i];
                }
            }
            openSet.Remove(currentNode);
            closedSet.Add(currentNode);



            if (currentNode == targetNode)
            {
                // return RetracePath_S(startNode, targetNode);
                return RetracePath_V(startNode, targetNode);
            }

            foreach (Node neighbour in grid.GetNeighbours(currentNode, FourDir))
            {
                if (neighbour.Type == Node.tile_type.WALL || closedSet.Contains(neighbour)) continue;

                int newMovementCostToNeighbour = currentNode.G_value + GetDistance(currentNode, neighbour);
                if(newMovementCostToNeighbour < neighbour.G_value || !openSet.Contains(neighbour))
                {
                    neighbour.G_value = newMovementCostToNeighbour;
                    neighbour.H_value = GetDistance(neighbour, targetNode);

                    neighbour.parent = currentNode;

                    if (!openSet.Contains(neighbour))
                        openSet.Add(neighbour);
                }
            }
          
        }

        //return "NONE";
        return startPos;
    }

    string RetracePath_S(Node startNode, Node endNode)
    {
        string Path = "";
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        while(currentNode != startNode)
        {
            path.Add(currentNode);

            if (currentNode.tilePosition - currentNode.parent.tilePosition == new Vector3(1, 0, 0)) // right
            {
                Path += "0";
            }
            if (currentNode.tilePosition - currentNode.parent.tilePosition == new Vector3(0, 1, 0)) // top
            {
                Path += "1";
            }
            if (currentNode.tilePosition - currentNode.parent.tilePosition == new Vector3(-1, 0, 0)) // left
            {
                Path += "2";
            }
            if (currentNode.tilePosition - currentNode.parent.tilePosition == new Vector3(0, -1, 0)) // down
            {
                Path += "3";
            }

            currentNode = currentNode.parent;
        }
        path.Reverse();

        string reversePath = "";
        for (int i = 0; i < Path.Length; i++ )
        {
            reversePath += Path[(Path.Length - 1 - i)];
        }

        return reversePath;
    }

    Vector3 RetracePath_V(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        path.Reverse();

        return path[0].tilePosition;
    }

    int GetDistance(Node nodeA, Node nodeB)
    {
        int dstX = Mathf.Abs((int)(nodeA.tilePosition.x - nodeB.tilePosition.x));
        int dstY = Mathf.Abs((int)(nodeA.tilePosition.y - nodeB.tilePosition.y));


        if(FourDir)
        {
            return (dstX + dstY) * 10;
        }
        else
        {
            if (dstX > dstY)
                return 14 * dstY + 10 * (dstX - dstY);
            else return 14 * dstX + 10 * (dstY - dstX);
        }
    }
}
