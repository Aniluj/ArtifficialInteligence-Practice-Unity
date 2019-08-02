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

public class PathFinderController : MonoBehaviour
{
    Node[,] nodeGrid;
    public Node originNode = new Node();
    public Node goalNode = new Node();
    private Node currentNode;

    public GameObject agent;
    public Transform destination;
    private Vector3 destinationPosition = new Vector3();
    private Vector3 agentPosition = new Vector3();

    public NodesGenerator nodesGenerator;
    private BreadthFirstSearch breadthFirstSearch = new BreadthFirstSearch();
    private DepthFirstSearch depthFirstSearch = new DepthFirstSearch();
    private Dijkstra dijkstra = new Dijkstra();
    private AStar aStar = new AStar();

    List<Vector3> pathToGoal = new List<Vector3>();
    List<Node> openedNodes = new List<Node>();

    public TypeOfPathFinding pathFindingType;
    public float movementSpeed;
    private bool goalFound;
    private int numbOfPositionsToReach;

    private void Awake()
    {
        //nodesGenerator = GetComponent<NodesGenerator>();
        nodesGenerator = GameObject.FindObjectOfType<NodesGenerator>().GetComponent<NodesGenerator>();
    }

    void Start ()
    {
        destinationPosition = destination.transform.position;
        agentPosition = agent.transform.position;

        nodesGenerator.genNodesGrid(ref nodeGrid);
        nodesGenerator.SetNearestNode(ref nodeGrid, ref agentPosition, ref originNode);
        Debug.Log("ORIGIN: " + originNode.position);
        nodesGenerator.SetNearestNode(ref nodeGrid, ref destinationPosition, ref goalNode);

        currentNode =  originNode;
        openedNodes.Add(currentNode);
        pathToGoal.Add(destinationPosition);

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

	}

    public void TravelToGoal()
    {
        if(goalFound == true)
        {
            GetPath();
            numbOfPositionsToReach = pathToGoal.Count - 1;
        }
        else if(numbOfPositionsToReach != -1)
        {
            agent.transform.position = Vector3.MoveTowards(agent.transform.position, pathToGoal[numbOfPositionsToReach], movementSpeed * Time.deltaTime);
            if(agent.transform.position == pathToGoal[numbOfPositionsToReach])
            {
                numbOfPositionsToReach--;
            }
        }
    }

    public void TravelToOrigin()
    {
        if(goalFound == false)
        {
            goalFound = true;
            numbOfPositionsToReach = 0;
            Debug.Log("Entra");
        }
        else if(numbOfPositionsToReach != pathToGoal.Count)
        {
            agent.transform.position = Vector3.MoveTowards(agent.transform.position, pathToGoal[numbOfPositionsToReach], movementSpeed * Time.deltaTime);
            if(agent.transform.position == pathToGoal[numbOfPositionsToReach])
            {
                numbOfPositionsToReach++;
            }
        }
    }
    
    void GetPath()
    {
        if(pathToGoal[0] != originNode.position)
        {
            if(currentNode.position != originNode.position)
            {
                pathToGoal.Add(currentNode.position);
                currentNode = currentNode.parent;
            }
            else
            {
                pathToGoal.Add(originNode.position);
                goalFound = false;
            }
        }
        else
        {
            goalFound = false;
        }
    }
}