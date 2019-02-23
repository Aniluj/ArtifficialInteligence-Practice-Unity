using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dijkstra {

    /*int counter = 0;
    Node auxNode;
    int secondCounter = 0;*/

    public void DijkstraAlgorithm(ref Node currentNode, ref List<Node> openedNodes, ref Node goalNode, ref bool goalFound) {
        currentNode.isOpen = true;
        while(openedNodes.Count != 0)
        {
            //counter = 0;
            if(currentNode.transform.position == goalNode.transform.position)
            {
                goalFound = true;
                return;
            }
            for(int i = 0; i < currentNode.adjacents.Count; i++)
            {
                if(currentNode.adjacents[i].isOpen == false && currentNode.adjacents[i].isClosed == false)
                {
                    //Debug.Log(currentNode.name + ": " + currentNode.adjacents[i].name);
                    currentNode.adjacents[i].isOpen = true;
                    openedNodes.Add(currentNode.adjacents[i]);
                    currentNode.adjacents[i].totalCost = 1 + currentNode.adjacents[i].cost;
                    currentNode.adjacents[i].parent = currentNode;
                    /*if(counter > 0)
                    {
                        if(currentNode.adjacents[i].totalCost < auxNode.totalCost)
                        {
                            auxNode = currentNode.adjacents[i];
                        }
                        else if (currentNode.adjacents[i].totalCost == auxNode.totalCost)
                        {
                            auxNode = currentNode.adjacents[i];
                        }
                    }
                    if(counter == 0)
                    {
                        auxNode = currentNode.adjacents[i];
                    }
                    counter++;*/
                }
            }
            Debug.Log(currentNode.name + ": " + currentNode.totalCost);
            
            /*for(int i = 0; i < openedNodes.Count; i++)
            {
                Debug.Log("iteracion " + secondCounter + ":" + openedNodes[i].name);
            }*/

            //openedNodes.RemoveAt(0);
            openedNodes.Remove(currentNode);
            currentNode.isOpen = false;
            currentNode.isClosed = true;
            //Debug.Log(auxNode.name + ": " + auxNode.totalCost);

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
            /*if(openedNodes.Count != 0)
            {
                currentNode = openedNodes[openedNodes.Count - 1];
                for(int i = 0; i < currentNode.parent.adjacents.Count; i++)
                {
                    if(currentNode.parent.adjacents[i].isOpen)
                    {
                        currentNode.parent.adjacents[i].totalCost = 1 + currentNode.parent.adjacents[i].cost;
                        if(currentNode.totalCost < currentNode.parent.adjacents[i].totalCost)
                        {

                        }
                    }
                }
                
            }*/
        }
    }
}
