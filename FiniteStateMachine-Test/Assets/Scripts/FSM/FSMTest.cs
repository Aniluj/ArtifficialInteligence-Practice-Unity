using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMTest : MonoBehaviour
{
    private const int goToTheMine = 0;
    private const int goToTheHouse = 1;
    private const int collectGold = 2;
    private const int leaveGold = 3;
    private const int stayStill = 4;
    private int goldLeft = 0;
    private int goldCollected = 1;
    private int positionReached = 2;
    private FSM finiteStatetMachine = new FSM(5, 3);
    private Vector3 direction;
    private Transform goldTransform;
    public Transform goldCarryingPoint;
    public Transform houseTransform;
    public Transform mineTransform;
    public float velocity = 0;

    void Start ()
    {
        direction.Set(mineTransform.position.x - transform.position.x, 0, mineTransform.position.z - transform.position.z);
    }
	
	void Update ()
    {
        switch (finiteStatetMachine.GetState())
        {
            case goToTheMine :
                transform.Translate((direction/direction.magnitude) * Time.deltaTime * velocity);
                if ((this.transform.position.x >= mineTransform.position.x))
                {
                    finiteStatetMachine.SetRelation(finiteStatetMachine.GetState(), positionReached, collectGold);
                    finiteStatetMachine.SetEvent(positionReached);
                }
                break;
            case collectGold:
                if (mineTransform.childCount != 0)
                {
                    mineTransform.Find("Gold").transform.SetParent(transform, true);
                    transform.Find("Gold").transform.localPosition = goldCarryingPoint.localPosition;
                    finiteStatetMachine.SetRelation(collectGold, goldCollected, goToTheHouse);
                    finiteStatetMachine.SetEvent(goldCollected);
                    direction.Set(houseTransform.position.x - transform.position.x, 0, houseTransform.position.z - transform.position.z);
                }
                else
                {
                    finiteStatetMachine.SetRelation(collectGold, goldCollected, stayStill);
                    finiteStatetMachine.SetEvent(goldCollected);
                }
                break;
            case goToTheHouse:
                transform.Translate((direction / direction.magnitude) * Time.deltaTime * velocity);
                if (transform.position.x <= houseTransform.position.x)
                {

                    finiteStatetMachine.SetRelation(goToTheHouse, positionReached, leaveGold);
                    finiteStatetMachine.SetEvent(positionReached);
                }
                break;
            case leaveGold:
                transform.Find("Gold").transform.SetParent(houseTransform);
                finiteStatetMachine.SetRelation(leaveGold, goldLeft, goToTheMine);
                finiteStatetMachine.SetEvent(goldLeft);
                direction.Set(mineTransform.position.x - transform.position.x, 0, mineTransform.position.z - transform.position.z);
                break;
            case stayStill:
                velocity = 0;
                break;
        }
	}
}
