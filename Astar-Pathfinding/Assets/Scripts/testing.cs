using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testing : MonoBehaviour
{
    //Variable declarations and initialization
    [SerializeField] private PathfindingVisual pathfindingVisual;
    private Pathfinding pathfinding;

    private int prevX = 0;
    private int prevY = 0;

    public int gridSize = 10;

    //Create our map of nodes
    void Start()
    {
        pathfinding = new Pathfinding(gridSize, gridSize);
        pathfindingVisual.SetGrid(pathfinding.GetMap());
    }

    //Whenever the user presses Left click, the path will be drawn.
    //If the user presses right click, an obstacle will be added.
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mouseWorldPosition = GetMouseWorldPosition();
            pathfinding.GetMap().GetXY(mouseWorldPosition, out int x, out int y);
            List<PathNode> path = pathfinding.FindPath(prevX, prevY, x, y);

            if(pathfinding.FindPath(prevX, prevY, x, y) != null)
            {
                prevX = x;
                prevY = y;
            }
            
            if (path != null)
            {
                for (int i = 0; i < path.Count - 1; i++)
                {
                    Debug.DrawLine(new Vector3(path[i].x, path[i].y) * 10f + Vector3.one * 5f, new Vector3(path[i + 1].x, path[i + 1].y) * 10f + Vector3.one * 5f, Color.green, 5f);
                }
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            Vector3 mouseWorldPosition = GetMouseWorldPosition();
            pathfinding.GetMap().GetXY(mouseWorldPosition, out int x, out int y);
            pathfinding.GetNode(x, y).SetIsWalkable(!pathfinding.GetNode(x, y).isWalkable);
        }
    }

    //Gets the position of our mouse in the world
    public static Vector3 GetMouseWorldPosition()
    {
        Vector3 vec3 = GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
        vec3.z = 0f;
        return vec3;
    }

    public static Vector3 GetMouseWorldPositionWithZ(Vector3 screenPosition, Camera worldCamera)
    {
        Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
        return worldPosition;
    }
}