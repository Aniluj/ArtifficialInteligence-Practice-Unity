using UnityEngine;
using System.Collections;

public class BT_SequenceAll<T> : BT_SequenceNode<T> where T : class
{
	protected override bool ShouldContinueOnChildState(State childState)
	{
		return true;
	}
	
	protected override State GetStateToReturn(int lastChildIndex, State lastIndexState)
	{
		return State.Success;
	}
	
	protected override State GetStateToReturnIterationFinished()
	{
		return State.Success;
	}
}
