using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMTest : MonoBehaviour {

    private FSM finiteStatetMachine = new FSM(4, 1);
    private const int moveRight = 0;
    private const int moveUp = 1;
    private const int moveLeft = 2;
    private const int moveDown = 3;
    private int changeDirection = 0;
    private int movementState = 0;
    public float velocity = 0;

    void Start () {
        
    }
	
	void Update () {

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            changeDirection = 0;
            movementState += 1;
            if (movementState == 4)
            {
                movementState = 0;
            }
            finiteStatetMachine.SetEvent(changeDirection);
            finiteStatetMachine.SetRelation(finiteStatetMachine.GetState(), changeDirection, movementState);
        }

        switch (finiteStatetMachine.GetState())
        {
            case moveRight :
                transform.Translate(Vector3.right * Time.deltaTime * velocity);
                break;
            case moveLeft:
                transform.Translate(Vector3.left * Time.deltaTime * velocity);
                break;
            case moveUp:
                transform.Translate(Vector3.up * Time.deltaTime * velocity);
                break;
            case moveDown:
                transform.Translate(Vector3.down * Time.deltaTime * velocity);
                break;
        }
	}
}
