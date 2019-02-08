using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PathFinderBFS : MonoBehaviour {

    public Node goalNode;
    public Node originNode;
    public GameObject agent;
    public float movementSpeed;
    private Node currentNode;
    private bool goalFound;
    private int i;
    Stack<Vector3> pathToGoal = new Stack<Vector3>();
    Queue<Node> openedNodes = new Queue<Node>();
    List<Node> closedNodes = new List<Node>();

    void Start ()
    {
        originNode.parent = null;
        currentNode = originNode;
        i = 0;
        BFS();
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
                pathToGoal.Pop();
            }
        }
	}

    void BFS()
    {
        currentNode.isOpen = true;
        openedNodes.Enqueue(currentNode);
        while(openedNodes.Count != 0)
        {
            if(currentNode.transform.position == goalNode.transform.position)
            {
                goalFound = true;
                break;
            }
            for(int i = 0; i < currentNode.adjacents.Count;i++)
            {
                if(currentNode.adjacents[i].isOpen == false && currentNode.adjacents[i].isClosed == false)
                {
                    currentNode.adjacents[i].isOpen = true;
                    openedNodes.Enqueue(currentNode.adjacents[i]);
                    currentNode.adjacents[i].parent = currentNode;
                }
            }
            openedNodes.Dequeue();
            currentNode.isOpen = false;
            currentNode.isClosed = true;
            closedNodes.Add(currentNode);
            if(openedNodes.Count != 0)
            {
                currentNode = openedNodes.Peek();
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
