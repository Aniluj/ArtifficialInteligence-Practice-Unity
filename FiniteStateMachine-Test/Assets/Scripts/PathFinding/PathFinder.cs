using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum TypeOfPathFinding
{
    BreadthFirstSearch,
    DepthFirstSearch,
    Dijkstra,
    AStar
};

public class PathFinder : MonoBehaviour
{
    public Node originNode;
    public Node goalNode;
    private Node currentNode;
    public GameObject agent;
    private BreadthFirstSearch breadthFirstSearch = new BreadthFirstSearch();
    private DepthFirstSearch depthFirstSearch = new DepthFirstSearch();
    private Dijkstra dijkstra = new Dijkstra();
    private AStar aStar = new AStar();
    List<Vector3> pathToGoal = new List<Vector3>();
    List<Node> openedNodes = new List<Node>();

    public TypeOfPathFinding pathFindingType;
    public float movementSpeed;
    private bool goalFound;
    private int aux;

    void Start ()
    {
        originNode.parent = null;
        currentNode = originNode;
        openedNodes.Add(currentNode);

        switch(pathFindingType)
        {
            case TypeOfPathFinding.BreadthFirstSearch:
                breadthFirstSearch.BFS(ref currentNode, ref openedNodes, ref goalNode, ref goalFound);
                break;
            case TypeOfPathFinding.DepthFirstSearch:
                depthFirstSearch.DFS(ref currentNode, ref openedNodes, ref goalNode, ref goalFound);
                break;
            case TypeOfPathFinding.Dijkstra:
                dijkstra.DijkstraAlgorithm(ref currentNode, ref openedNodes, ref goalNode, ref goalFound);
                break;
            case TypeOfPathFinding.AStar:
                aStar.AStarAlgorithm(ref currentNode, ref openedNodes, ref goalNode, ref goalFound);
                break;
            default:
                break;
        }
    }

	void Update ()
    {
        if(goalFound == true)
        {
            GetPath();
            aux = pathToGoal.Count - 1;
        }
        if(aux != -1)
        {
            agent.transform.position = Vector3.MoveTowards(agent.transform.position, pathToGoal[aux], movementSpeed * Time.deltaTime);
            if(agent.transform.position == pathToGoal[aux])
            {
                aux--;
            }
        }
	}

    void GetPath()
    {
        if(currentNode.position != originNode.position)
        {
            pathToGoal.Add(currentNode.position);
            currentNode = currentNode.parent;
        }
        else
        {
            goalFound = false;
        }
    }
}