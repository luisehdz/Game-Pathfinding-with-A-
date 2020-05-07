# Game-Pathfinding-with-Astar

## What is A* and why should I know about it?
A* is one of the most robust and widely known searching algorithms. It uses "common sense" to find the shortest path to the goal from a starting position. It is useful in many
different scenarios and its a very powerful algorithm to know. The scenario that I will be sharing with you will be in Video Games.

## How does A* work and why is it different from other searching algorithms?
A* works differently than other searching algorithms by calculating the shortest route to the goal. Imagine a grid of squares which I will call nodes.
In between all of these nodes are edges that connect to each other that have different weights to them. Instead of of checking every single possible route with the lowest 
cost(shortest weight in between nodes), A* will choose the node with the lowest "f cost", which is the sum of the node's "g cost" and "h cost."
    
    f cost: the sum of the g cost and h cost. The lowest cost will be chosen
    g cost: the sum of all the cost of the edges that it has taken to get to the current node
    h cost: a theoretical cost of the node if it could go straight to the end node without any obstacles. "As the crow flies"

As for how this algorithm knows when a node has been chosen or traversed, we create 2 lists; an open and a closed list. Lets add our starting node to the open list.


## Why is this useful and where can I see this in the real world?

## Source Material with Extra Goodies + References

A Youtube Series tutorial and explanation by Code Monkey.

    https://www.youtube.com/playlist?list=PLzDRvYVwl53uhO8yhqxcyjDImRjO9W722

A General explanation and rundown of the A* Algorithm with code in c++

    https://www.geeksforgeeks.org/a-search-algorithm/

"Introduction to Genetic Algorithms — Including Example Code" by Vijini Mallawaarachchi.

    https://www.redblobgames.com/pathfinding/a-star/introduction.html

Thanks for Reading. I hope you learned a few things for your future projects!