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
    private int noGold = 3;
    private FSM finiteStatetMachine = new FSM(5, 4);
    private PathFinderController pathFinderController;
    private Vector3 direction;
    private Transform goldTransform;
    public Transform goldCarryingPoint;
    public Transform houseTransform;
    public Transform mineTransform;
    public float velocity = 0;

    private void Awake()
    {
        pathFinderController = GetComponent<PathFinderController>();
    }

    void Start ()
    {
        finiteStatetMachine.SetRelation(goToTheMine, positionReached, collectGold);
        finiteStatetMachine.SetRelation(collectGold, goldCollected, goToTheHouse);
        finiteStatetMachine.SetRelation(goToTheHouse, positionReached, leaveGold);
        finiteStatetMachine.SetRelation(leaveGold, goldLeft, goToTheMine);
        finiteStatetMachine.SetRelation(leaveGold, noGold, stayStill);

        direction.Set(mineTransform.position.x - transform.position.x, 0, mineTransform.position.z - transform.position.z);
    }
	
	void Update ()
    {
        switch (finiteStatetMachine.GetState())
        {
            case goToTheMine :
                //transform.Translate((direction/direction.magnitude) * Time.deltaTime * velocity);
                pathFinderController.TravelToGoal();
                if (transform.position.x == pathFinderController.goalNode.position.x && transform.position.z == pathFinderController.goalNode.position.z)
                {
                    finiteStatetMachine.SetEvent(positionReached);
                }
                break;
            case collectGold:
                if (mineTransform.childCount != 0)
                {
                    mineTransform.Find("Gold").transform.SetParent(transform, true);
                    transform.Find("Gold").transform.localPosition = goldCarryingPoint.localPosition;
                    finiteStatetMachine.SetEvent(goldCollected);
                    direction.Set(houseTransform.position.x - transform.position.x, 0, houseTransform.position.z - transform.position.z);
                }
                break;
            case goToTheHouse:
                //transform.Translate((direction / direction.magnitude) * Time.deltaTime * velocity);
                pathFinderController.TravelToOrigin();
                if (transform.position.x == pathFinderController.originNode.position.x && transform.position.z == pathFinderController.originNode.position.z)
                {
                    finiteStatetMachine.SetEvent(positionReached);
                }
                break;
            case leaveGold:
                transform.Find("Gold").transform.SetParent(houseTransform);
                if(mineTransform.childCount != 0)
                {
                    finiteStatetMachine.SetEvent(goldLeft);
                }
                else
                {
                    finiteStatetMachine.SetEvent(noGold);
                }
                direction.Set(mineTransform.position.x - transform.position.x, 0, mineTransform.position.z - transform.position.z);
                break;
            case stayStill:
                //velocity = 0;
                pathFinderController.TravelToOrigin();
                break;
        }
	}
}
