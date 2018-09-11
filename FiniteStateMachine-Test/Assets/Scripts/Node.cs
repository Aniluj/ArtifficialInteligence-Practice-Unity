using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour {

    public List<Node> adjacents;
    public float cost;
    public float totalCost;
    public bool isOpen;
    public bool isClosed;
    public Node parent;
	
	void Start () {
        isOpen = false;
        isClosed = false;
	}
	
	
	void Update () {
		
	}
}
