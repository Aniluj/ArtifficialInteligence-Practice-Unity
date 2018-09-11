using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinderBFS : MonoBehaviour {

    Node goalNode;
    Node originNode;
    Node currentNode;
    List<Vector3> pathToGoal = new List<Vector3>();
    List<Node> openedNodes = new List<Node>();
    List<Node> closedNodes = new List<Node>();

    private void Awake()
    {
        
    }

    void Start ()
    {
        currentNode = originNode;
	}
	

	void Update ()
    {
		
	}

    void BFS()
    {
        while(openedNodes.Count != 0)
        {
            currentNode.isClosed = true;
            pathToGoal.Add(currentNode.transform.position);
            closedNodes.Add(currentNode);
            for(int i = 0; i < currentNode.adjacents.Count; i++)
            {
                if(currentNode.adjacents[i].isOpen == false && currentNode.adjacents[i].isClosed == false)
                {
                    currentNode.adjacents[i].isOpen = true;
                    openedNodes.Add(currentNode.adjacents[i]);
                }
            }
            for(int i=0; i<currentNode.adjacents.Count;i++)
            {
                if(currentNode.adjacents[i].isOpen == false && currentNode.adjacents[i].isClosed == false)
                { 
                    currentNode.adjacents[i].isOpen = true;
                    currentNode.adjacents[i].parent = currentNode;
                    openedNodes.Add(currentNode.adjacents[i]);
                }
                if(currentNode.adjacents[i].isOpen == true)
                {
                    currentNode = currentNode.adjacents[i];
                    break;
                }
            }
        }
    }
}
