# Game-Pathfinding-with-Astar
This was made with Unity 2019.2.2f1 using a custom made grid-based system.

## What is A* and why should I know about it?
A* is one of the most robust and widely known searching algorithms. It uses "common sense" to find the shortest path to the goal from a starting position. It is useful in many
different scenarios and its a very powerful algorithm to know. The scenario that I will be sharing with you will be in Video Games.

## How does A* work and why is it different from other searching algorithms?
A* works differently than other searching algorithms by calculating the distance it has already taken to get to its current point, "g cost." Imagine a grid of squares which I will call nodes.
In between all of these nodes are edges that connect to each other that have different weights to them. Instead of of checking every single possible route with the lowest 
cost(shortest weight in between nodes), A* will choose the node with the lowest "f cost", which is the sum of the node's "g cost" and "h cost."
    
    f cost: the sum of the g cost and h cost. The lowest cost will be chosen
    g cost: the sum of all the cost of the edges that it has taken to get to the current node
    h cost: a theoretical cost of the node if it could go straight to the end node without any obstacles. "As the crow flies"

As for how this algorithm knows when a node has been chosen or traversed, we create 2 lists; an open and a closed list. Lets add our starting node to the open list.

### Step-by-step
While the open list is not empty, find the node with the lowest f cost and remove it from the open list. For simplicity sake, lets call it the "current" node.
If this current node is the end goal, then the algorithm has finished traversing, return the current node. If this node is not the end node, then instead add this node to the closed list.
Then, for each neighbor of the current node that isn't on the CLOSED list, assign the neighbor node as the current node. Calculate the f cost, g, cost, and h cost of this node.
Add this node to the open list. However, if the neighbor node chosen is on the OPEN list, check to see if the g cost coming from this node is less than the current node.
If it is, then set the neighbor's parent to the current node. Recalculate the f cost using this new node's g cost. Repeat this process until either all nodes are exhausted or if we reach the end node.


#### Calculating h cost
There are a few different ways to find the answer to this problem. We can either get an exact approximation or a good enough estimate. I will be covering the latter.

    Manhattan Distance: the sum of absolute values of differences in the goal’s x and y coordinates and the current cell’s x and y coordinates
        h = abs(currentNode.x – endNode.x) + abs (currentNode.y – endNode.y);
        This should be used when the AI can only move in 4 directions.
    Diagonal Distance: the maximum of absolute values of differences in the goal’s x and y coordinates and the current cell’s x and y coordinates
        h = max { abs(currentNode.x – endNode.x), abs(currentNode.y – endNode.y) };
        This should be used when the AI can move omnidirectionally(8 directions). It is also the algorithm used in this project.
    Euclidean Distance: the distance formula
        h = sqrt ( (currentNode.x – endNode.x) ^ 2 + (currentNode.y – goal.y) ^ 2 );
        This should be used when the AI can move in any direction.


## Why is this useful and where can I see this in the real world?
You don't have to think very long on where you can find this algorithm in the real world. Self-driving cars, GPS, AI robots, and even in video games are places where you can find this algorithm in use.
Generally, the most places you'll see this algorithm is in AI robots. Whether it be in video games, or in real life, A* is a good starting base for problems that require traversing an area with point A and B.

## Source Material with Extra Goodies + References

A Youtube Series tutorial and explanation by Code Monkey.

    https://www.youtube.com/playlist?list=PLzDRvYVwl53uhO8yhqxcyjDImRjO9W722

A General explanation and rundown of the A* Algorithm with code in c++

    https://www.geeksforgeeks.org/a-search-algorithm/

"Introduction to Genetic Algorithms — Including Example Code" by Vijini Mallawaarachchi.

    https://www.redblobgames.com/pathfinding/a-star/introduction.html


Thanks for reading. I hope you learned a few things for your future projects!