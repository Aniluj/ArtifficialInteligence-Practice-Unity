using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public Vector3 position = new Vector3();
    public List<Node> adjacents = new List<Node>();
    public float cost = 1f;
    public float totalCost = 0f;
    public bool isOpen;
    public bool isClosed;
    public Node parent = null;
	
	void Start () {
        isOpen = false;
        isClosed = false;
	}
	
	
	void Update () {
		
	}
}
