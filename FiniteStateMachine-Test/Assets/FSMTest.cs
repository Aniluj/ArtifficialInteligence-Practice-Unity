using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMTest : MonoBehaviour {

    FSM fineStateMachine = new FSM(4, 1);
    const int moveRight = 0;
    const int moveUp = 1;
    const int moveLeft = 2;
    const int moveDown = 3;
    Transform playerTransfor;
    int changeDirectionMovement = 1;

    void Start () {
        
    }
	
	void Update () {

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            changeDirectionMovement = 1;
            fineStateMachine.SetEvent(changeDirectionMovement);
        }

        switch (fineStateMachine.GetState())
        {
            case moveRight :
                transform.Translate(Vector3 right(1, 0, 0));
                break;
            case moveLeft:
                Debug.Log("fsafsa");
                break;
        }
	}
}
