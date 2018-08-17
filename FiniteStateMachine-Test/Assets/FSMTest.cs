using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMTest : MonoBehaviour {

    private FSM finiteStatetMachine = new FSM(4, 1);
    private const int moveRight = 0;
    private const int moveUp = 1;
    private const int moveLeft = 2;
    private const int moveDown = 3;
    private int movement = 0;
    private int moving = 1;
    private int state = 0;
    public float velocity = 0;

    void Start () {
        
    }
	
	void Update () {

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            movement = 0;
            state += 1;
            if (state == 4)
            {
                state = 0;
            }
            finiteStatetMachine.SetRelation(state, movement, moving);
        }

        switch (finiteStatetMachine.GetState())
        {
            case moveRight :
                transform.Translate(Vector3.right*Time.deltaTime*velocity);
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
