using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM
{
    private int[,] fsm;
    private int state;
    private int initialState = 0;

    public FSM(int statesCount, int eventsCount)
    {
        for (int i = 0; i < eventsCount; i++)
        {
            for (int j = 0; j < eventsCount; j++)
            {
                fsm[i, j] = -1;
            }
        }
        state = initialState;
    }

    public void SetRelation(int srcState, int eventId, int destinationState)
    {
        fsm[srcState, eventId] = destinationState;
    }

    public int GetState()
    {
        return state;
    }

    public void SetEvent(int eventId)
    {
        if (fsm[state, eventId] != -1)
        {
            state = fsm[state, eventId];
        }
    }
}
