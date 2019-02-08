using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PathFinder : MonoBehaviour {

    public Node goalNode;
    public Node originNode;
    public GameObject agent;
    public float movementSpeed;
    private Node currentNode;
    private bool goalFound;
    Stack<Vector3> pathToGoal = new Stack<Vector3>();
    List<Node> openedNodes = new List<Node>();
    List<Node> closedNodes = new List<Node>();

    void Start ()
    {
        originNode.parent = null;
        currentNode = originNode;
        openedNodes.Add(currentNode);
        DFS();
	}

	void Update ()
    {
        if(goalFound == true)
        {
            GetPath();
        }
        if(pathToGoal.Count != 0)
        {
            agent.transform.position = Vector3.MoveTowards(agent.transform.position, pathToGoal.Peek(), movementSpeed * Time.deltaTime);
            if(agent.transform.position == pathToGoal.Peek())
            {
                Debug.Log(pathToGoal.Peek());
                pathToGoal.Pop();
            }
        }
	}

    void BFS()
    {
        currentNode.isOpen = true;
        while(openedNodes.Count != 0)
        {
            if(currentNode.transform.position == goalNode.transform.position)
            {
                goalFound = true;
                return;
            }
            for(int i = 0; i < currentNode.adjacents.Count;i++)
            {
                if(currentNode.adjacents[i].isOpen == false && currentNode.adjacents[i].isClosed == false)
                {
                    currentNode.adjacents[i].isOpen = true;
                    openedNodes.Add(currentNode.adjacents[i]);
                    currentNode.adjacents[i].parent = currentNode;
                }
            }
            openedNodes.RemoveAt(0);
            currentNode.isOpen = false;
            currentNode.isClosed = true;
            closedNodes.Add(currentNode);
            if(openedNodes.Count != 0)
            {
                currentNode = openedNodes[0];
            }
        }
    }

    void DFS() {
        currentNode.isOpen = true;
        while(openedNodes.Count != 0)
        {
            if(currentNode.transform.position == goalNode.transform.position)
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
                    currentNode.adjacents[i].parent = currentNode;
                }
            }
            openedNodes.RemoveAt(0);
            currentNode.isOpen = false;
            currentNode.isClosed = true;
            closedNodes.Add(currentNode);
            if(openedNodes.Count != 0)
            {
                currentNode = openedNodes[openedNodes.Count - 1];
            }
        }
    }

    void GetPath()
    {
        if(currentNode.transform.position != originNode.transform.position)
        {
            pathToGoal.Push(currentNode.transform.position);
            currentNode = currentNode.parent;
        }
        else
        {
            goalFound = false;
        }
    }
}
