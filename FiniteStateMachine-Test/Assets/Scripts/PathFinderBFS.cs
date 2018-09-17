using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PathFinderBFS : MonoBehaviour {

    public Node goalNode;
    public Node originNode;
    private Node currentNode;
    List<Vector3> pathToGoal = new List<Vector3>();
    List<Node> openedNodes = new List<Node>();
    List<Node> closedNodes = new List<Node>();

    private void Awake()
    {
        
    }

    void Start ()
    {
        currentNode = originNode;
        BFS();
	}
	

	void Update ()
    {
        
	}

    void BFS()
    {
        if (currentNode == originNode)
        {
            pathToGoal.Add(currentNode.transform.position);
            closedNodes.Add(currentNode);
        }
        for (int i = 0; i < currentNode.adjacents.Count; i++)
        {
            if (currentNode.adjacents[i].isOpen == false && currentNode.adjacents[i].isClosed == false)
            {
                currentNode.adjacents[i].isOpen = true;
                openedNodes.Add(currentNode.adjacents[i]);
            }
        }
        while (openedNodes.Count != 0)
        {
            currentNode.isClosed = true;
            if(currentNode.parent != null)
            {
                Debug.Log(currentNode.parent.transform.position);
                pathToGoal.Add(currentNode.parent.transform.position);
            }
            if(currentNode == goalNode)
            {
                break;
            }
            for (int i = 0; i < currentNode.adjacents.Count; i++)
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
