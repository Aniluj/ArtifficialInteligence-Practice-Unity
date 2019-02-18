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
    private BreadthFirstSearch breadthFirstSearch = new BreadthFirstSearch();
    private DepthFirstSearch depthFirstSearch = new DepthFirstSearch();
    private Dijkstra dijkstra = new Dijkstra();
    Stack<Vector3> pathToGoal = new Stack<Vector3>();
    List<Node> openedNodes = new List<Node>();
    List<Node> closedNodes = new List<Node>();

    void Start ()
    {
        originNode.parent = null;
        currentNode = originNode;
        openedNodes.Add(currentNode);
        //breadthFirstSearch.BFS(ref currentNode, ref openedNodes, ref goalNode, ref goalFound);
        //depthFirstSearch.DFS(ref currentNode, ref openedNodes, ref goalNode, ref goalFound);
        dijkstra.DijkstraAlgorithm(ref currentNode, ref openedNodes, ref goalNode, ref goalFound);
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
                //Debug.Log(pathToGoal.Peek());
                pathToGoal.Pop();
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