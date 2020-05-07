using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This creates our basic network of nodes that we will use to traverse the map with
 * It includes our basic variables for the A* algorithm such as gCost, hCost, and fCost
 * Tutorial Followed by CodeMonkey
 */
public class PathNode
{
    private GridSystem<PathNode> gridNetwork;
    public int x;
    public int y;

    public int gCost;
    public int hCost;
    public int fCost;

    public bool isWalkable;
    public PathNode cameFromNode;

    public PathNode(GridSystem<PathNode> map, int x, int y)
    {
        this.gridNetwork = map;
        this.x = x;
        this.y = y;
        isWalkable = true;
    }

    //This calculates the F Cost
    public void CalculateFCost()
    {
        fCost = gCost + hCost;
    }

    //This sets if a tile is walkable or not
    public void SetIsWalkable(bool isWalkable)
    {
        this.isWalkable = isWalkable;
        gridNetwork.TriggerGridObjectChanged(x, y);
    }

    //This gives prints out the position of the tile
    public override string ToString()
    {
        //return x + "," + y;
        return " ";
    }
}
