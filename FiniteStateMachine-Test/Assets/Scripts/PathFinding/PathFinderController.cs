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

    private NodesGenerator nodesGenerator;
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

    private void Awake()
    {
        nodesGenerator = GetComponent<NodesGenerator>();
    }

    void Start ()
    {
        destinationPosition = destination.transform.position;
        agentPosition = agent.transform.position;

        nodesGenerator.genNodesGrid(ref nodeGrid);
        nodesGenerator.SetNearestNode(ref nodeGrid, ref agentPosition, ref originNode);
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
                Debug.Log(pathToGoal[aux]);
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
            pathToGoal.Add(agentPosition);
            goalFound = false;
        }
    }
}