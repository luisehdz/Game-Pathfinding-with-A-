using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This is the pathfinding class which we use to find a path from point A to B
//Tutorial followed by Codemonkey and modified by me
public class Pathfinding
{
    //constant variable for moving
    private const int MOVE_STRAIGHT_COST = 10;
    private const int MOVE_DIAGONAL_COST = 14;

    //Initialized variables for our lists and map
    private GridSystem<PathNode> map;
    private List<PathNode> openList;
    private List<PathNode> closedList;

    //Creates our map
    public Pathfinding(int width, int height)
    {
        map = new GridSystem<PathNode>(width, height, 10f, Vector3.zero, (GridSystem<PathNode> g, int x, int y) => new PathNode(g, x, y));
    }

    //Getter function for our map
    public GridSystem<PathNode> GetMap()
    {
        return map;
    }

    /*
     * This is the meat and bones of the algorithm.
     * We get our starting node on the map and then see where the user pressed to move (end goal).
     * To get to where the end goal is, we first check our neighbor nodes to see which on has the lowest fCost.
     * For every neighbor we check we put it on the open list. We then find which neighbor has the lowest fCost and remove it from the open list.
     * Lets call the node we removed our "current node" If this node is the destination then we return. 
     * If it is not, then we put this node in the closed list. Then we check the current node's neighbors not on the closed list and calculate again.
     * If we are to find a find a neighbor node thats on the open list with a shorter g cost we swap over to that path instead.
     */
    public List<PathNode> FindPath(int startX, int startY, int endX, int endY)
    {
        PathNode startNode = map.GetGridObject(startX, startY);
        PathNode endNode = map.GetGridObject(endX, endY);

        openList = new List<PathNode> { startNode };
        closedList = new List<PathNode>();

        for(int x = 0; x < map.GetWidth(); x++)
        {
            for(int y = 0; y < map.GetHeight(); y++)
            {
                PathNode pathNode = map.GetGridObject(x, y);
                pathNode.gCost = int.MaxValue;
                pathNode.CalculateFCost();
                pathNode.cameFromNode = null;
            }
        }

        startNode.gCost = 0;
        startNode.hCost = CalculateDistanceCost(startNode, endNode);
        startNode.CalculateFCost();

        while(openList.Count > 0)
        {
            PathNode currentNode = GetLowestFCostNode(openList);
            if(currentNode == endNode)
            {
                return CalculatePath(endNode);
            }

            openList.Remove(currentNode);
            closedList.Add(currentNode);

            foreach (PathNode neighborNode in GetNeighborList(currentNode))
            {
                if (closedList.Contains(neighborNode)) continue;
                if(!neighborNode.isWalkable)
                {
                    closedList.Add(neighborNode);
                    continue;
                }

                int tempGCost = currentNode.gCost + CalculateDistanceCost(currentNode, neighborNode);
                if(tempGCost < neighborNode.gCost)
                {
                    neighborNode.cameFromNode = currentNode;
                    neighborNode.gCost = tempGCost;
                    neighborNode.hCost = CalculateDistanceCost(neighborNode, endNode);
                    neighborNode.CalculateFCost();

                    if(!openList.Contains(neighborNode))
                    {
                        openList.Add(neighborNode);
                    }
                }
            }
        }

        //Out of nodes / Path not Found
        return null;
    }

    //This gives us a listof all the neighbors a node could have
    private List<PathNode> GetNeighborList(PathNode currentNode)
    {
        List<PathNode> neighbourList = new List<PathNode>();

        if (currentNode.x - 1 >= 0 && GetNode(currentNode.x - 1, currentNode.y).isWalkable)
        {
            // Left
            neighbourList.Add(GetNode(currentNode.x - 1, currentNode.y));
            // Left Down
            if (currentNode.y - 1 >= 0)
            {
                neighbourList.Add(GetNode(currentNode.x - 1, currentNode.y - 1));
            }

            // Left Up
            if (currentNode.y + 1 < map.GetHeight())
            {
                neighbourList.Add(GetNode(currentNode.x - 1, currentNode.y + 1));
            }
        }

        if (currentNode.x + 1 < map.GetWidth() && GetNode(currentNode.x + 1, currentNode.y).isWalkable)
        {
            // Right
            neighbourList.Add(GetNode(currentNode.x + 1, currentNode.y));
            // Right Down
            if (currentNode.y - 1 >= 0)
            {
                neighbourList.Add(GetNode(currentNode.x + 1, currentNode.y - 1));
            }

            // Right Up
            if (currentNode.y + 1 < map.GetHeight())
            {
                neighbourList.Add(GetNode(currentNode.x + 1, currentNode.y + 1));
            }
        }

        // Down
        if (currentNode.y - 1 >= 0 && GetNode(currentNode.x, currentNode.y - 1).isWalkable)
        {
            neighbourList.Add(GetNode(currentNode.x, currentNode.y - 1));
        }

        // Up
        if (currentNode.y + 1 < map.GetHeight() && GetNode(currentNode.x, currentNode.y + 1).isWalkable)
        {
            neighbourList.Add(GetNode(currentNode.x, currentNode.y + 1));
        }

        return neighbourList;
    }

    //Getter function to get a node
    public PathNode GetNode(int x, int y)
    {
        return map.GetGridObject(x, y);
    }

    //This calculates our path once we have found from start to end.
    private List<PathNode> CalculatePath(PathNode endNode)
    {
        List<PathNode> path = new List<PathNode>();
        path.Add(endNode);
        PathNode currentNode = endNode;
        while (currentNode.cameFromNode != null)
        {
            path.Add(currentNode.cameFromNode);
            currentNode = currentNode.cameFromNode;
        }

        path.Reverse();
        return path;
    }

    //This gives us our heuristic cost
   private int CalculateDistanceCost(PathNode a, PathNode b)
    {
        int xDistance = Mathf.Abs(a.x - b.x);
        int yDistance = Mathf.Abs(a.y - b.y);
        int remaining = Mathf.Abs(xDistance - yDistance);
        return MOVE_DIAGONAL_COST * Mathf.Min(xDistance, yDistance) + MOVE_STRAIGHT_COST * remaining;
    }

    //This returns the lowest F cost node in a list of nodes
    private PathNode GetLowestFCostNode(List<PathNode> pathNodeList)
    {
        PathNode lowestFCostNode = pathNodeList[0];
        for(int i = 1; i < pathNodeList.Count; i++)
        {
            if(pathNodeList[i].fCost < lowestFCostNode.fCost)
            {
                lowestFCostNode = pathNodeList[i];
            }
        }

        return lowestFCostNode;
    }
}