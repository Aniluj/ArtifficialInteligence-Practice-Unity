using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodesGenerator : MonoBehaviour {

    Node[,] nodeGrid;
    public int rows;
    public int columns;
    public float distanceBetweenNodes;

    public void genNodesGrid()
    {
        Node[,] nodeGrid = new Node[rows , columns];

        for(int i = 0; i < rows; i++)
        {
            for(int j=0; j < columns; j++)
            {
                nodeGrid[i, j] = new Node();

                nodeGrid[i, j].position = new Vector3
                    (
                    transform.position.x + (i * distanceBetweenNodes),
                    transform.position.y,
                    transform.position.z + (j * distanceBetweenNodes)
                    );
            }
        }

        for(int i = 0; i < rows; i++)
        {
            for(int j = 0; j < columns; j++)
            {
                if(j + 1 < columns)
                {
                    nodeGrid[i, j].adjacents.Add(nodeGrid[i, j + 1]);
                }
                if(j - 1 > 0)
                {
                    nodeGrid[i, j].adjacents.Add(nodeGrid[i, j - 1]);
                }
                if(i + 1 < rows)
                {
                    nodeGrid[i, j].adjacents.Add(nodeGrid[i + 1, j]);
                }
                if(i - 1 > 0)
                {
                    nodeGrid[i, j].adjacents.Add(nodeGrid[i - 1, j]);
                }
            }
        }
    }

}
