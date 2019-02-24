using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dijkstra
{
    public void DijkstraAlgorithm(ref Node currentNode, ref List<Node> openedNodes, ref Node goalNode, ref bool goalFound)
    {
        currentNode.isOpen = true;
        while(openedNodes.Count != 0)
        {
            if(currentNode.position == goalNode.position)
            {
                goalFound = true;
                return;
            }
            for(int i = 0; i < currentNode.adjacents.Count; i++)
            {
                if(currentNode.adjacents[i].isOpen == false && currentNode.adjacents[i].isClosed == false)
                {
                    currentNode.adjacents[i].isOpen = true;
                    openedNodes.Add(currentNode.adjacents[i]);
                    currentNode.adjacents[i].totalCost = 1 + currentNode.adjacents[i].cost;
                    currentNode.adjacents[i].parent = currentNode;
                }
            }

            openedNodes.Remove(currentNode);
            currentNode.isOpen = false;
            currentNode.isClosed = true;

            if(openedNodes.Count != 0)
            {
                currentNode = openedNodes[0];

                foreach(Node auxnode in openedNodes)
                {
                    if(auxnode.totalCost < currentNode.totalCost)
                    {
                        currentNode = auxnode;
                    }
                }
            }
        }
    }
}
