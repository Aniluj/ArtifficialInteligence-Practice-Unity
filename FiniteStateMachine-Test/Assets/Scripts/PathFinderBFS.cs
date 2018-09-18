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
    List<Vector3> pathToGoal = new List<Vector3>();
    List<Node> openedNodes = new List<Node>();
    List<Node> closedNodes = new List<Node>();

    private void Awake()
    {
        
    }

    void Start ()
    {
        currentNode = originNode;
        i = 0;
        BFS();
	}
	

	void Update ()
    {
        i = pathToGoal.Count-1;
        if(goalFound)
        {
            agent.transform.localPosition = Vector3.MoveTowards(agent.transform.position, pathToGoal[i], movementSpeed * Time.deltaTime);
            if(agent.transform.position == pathToGoal[i])
            {
                i--;
            }
            if(i<0)
            {
                goalFound = false;
            }
        }
	}

    void BFS()
    {
        currentNode.isOpen = true;
        openedNodes.Add(currentNode);
        currentNode.parent = originNode;
        while (openedNodes.Count != 0)
        {
            currentNode.isOpen = false;
            currentNode.isClosed = true;
            openedNodes.Remove(currentNode);
            closedNodes.Add(currentNode);
            pathToGoal.Add(currentNode.parent.transform.position);
            if(currentNode == goalNode)
            {
                Debug.Log(currentNode.transform.localPosition);
                pathToGoal.Add(currentNode.transform.position);
                goalFound = true;
                break;
            }
            for(int i=0; i<currentNode.adjacents.Count; i++)
            {
                if(currentNode.adjacents[i].isOpen != true && currentNode.adjacents[i].isClosed != true)
                {
                    currentNode.adjacents[i].isOpen = true;
                    currentNode.adjacents[i].parent = currentNode;
                    openedNodes.Add(currentNode.adjacents[i]);
                }
            }
            if(openedNodes.Count != 0)
            {
                currentNode = openedNodes[0];
            }
        }
    }
}
