using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodesGenerator : MonoBehaviour
{
    public int rows;
    public int columns;
    public float distanceBetweenNodes;
    public GameObject nodePrefab;

    private bool CollisionInBetween(Node a, Node b)
    {
        bool collides = Physics.Linecast(a.position, b.position);
        //Debug.Log(collides);
        return collides;
    }

    public void genNodesGrid(ref Node[,] nodeGrid)
    {
        nodeGrid = new Node[rows, columns];

        for(int i = 0; i < rows; i++)
        {
            for(int j=0; j < columns; j++)
            {
                Node nodeToAdd = new Node();
                nodeGrid[i, j] = nodeToAdd;

                nodeGrid[i, j].cost = Random.Range(0, 10);
                nodeGrid[i, j].position = new Vector3
                    (
                    transform.position.x + (i * distanceBetweenNodes),
                    transform.position.y,
                    transform.position.z + (j * distanceBetweenNodes)
                    );
                Instantiate(nodePrefab, nodeGrid[i, j].position, Quaternion.identity);
            }
        }


        for(int i = 0; i < rows; i++)
        {
            for(int j = 0; j < columns; j++)
            {
                if(j + 1 < columns && CollisionInBetween(nodeGrid[i, j], nodeGrid[i, j + 1]) == false)
                {
                    nodeGrid[i, j].adjacents.Add(nodeGrid[i, j + 1]);
                }
                if(j - 1 > 0 && CollisionInBetween(nodeGrid[i, j], nodeGrid[i, j - 1]) == false)
                {
                    nodeGrid[i, j].adjacents.Add(nodeGrid[i, j - 1]);
                }
                if(i + 1 < rows && CollisionInBetween(nodeGrid[i, j], nodeGrid[i + 1, j]) == false)
                {
                    nodeGrid[i, j].adjacents.Add(nodeGrid[i + 1, j]);
                }
                if(i - 1 > 0 && CollisionInBetween(nodeGrid[i, j], nodeGrid[i - 1, j]) == false)
                {
                    nodeGrid[i, j].adjacents.Add(nodeGrid[i - 1, j]);
                }
            }
        }
    }

    public void SetNearestNode(ref Node[,] nodeGrid, ref Vector3 firstPosition, ref Node nearestNode)
    {
        Vector3 distanceVector = new Vector3
            (
            nodeGrid[rows - 1, columns - 1].position.x - nodeGrid[0, 0].position.x,
            nodeGrid[0, 0].position.y,
            nodeGrid[rows - 1, columns - 1].position.z - nodeGrid[0, 0].position.z
            );

        float distance = distanceVector.magnitude;

        for(int i = 0; i < rows; i++)
        {
            for(int j = 0; j < columns; j++)
            {
                distanceVector = new Vector3
                    (
                    nodeGrid[i, j].position.x - firstPosition.x,
                    nodeGrid[i, j].position.y,
                    nodeGrid[i, j].position.z - firstPosition.z
                    );

                if(distanceVector.magnitude < distance)
                {
                    //Debug.Log(" MENOR DISTANCIA I" + i + ", J" + j + " " + distanceVector.magnitude);
                    Vector3 nodePositionXZ = new Vector3(nodeGrid[i, j].position.x, firstPosition.y, nodeGrid[i, j].position.z);
                    bool collision = Physics.Linecast(firstPosition, nodePositionXZ);

                    if(collision == false)
                    {
                        distance = distanceVector.magnitude;
                        nearestNode = nodeGrid[i, j];
                    }
                }
            }
        }
    }
}
